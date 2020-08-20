using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    public static class Checker
    {
        private static ILookup<Type, (MethodInfo method, string description)> _checksByType;

        private static ILookup<Type, (Delegate method, string description)> _checkDelegatesByType;

        static Checker()
        {
            _checksByType = _getChecksByType();
            _checkDelegatesByType = _getCheckDelegatesByType();
        }

        public static void PrintToConsole(IEnumerable<(string test, string result)> report)
        {
            foreach (var item in report)
            {
                var result = item.result;
                var success = string.IsNullOrWhiteSpace(result);

                Console.ForegroundColor = success ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write(item.test+ " ");

                if (success)
                {
                    Console.WriteLine("Check!");
                }
                else
                {
                    Console.WriteLine($"Fail: {result}");
                }
            }
        }

        public static IEnumerable<(string test, string result)> CheckByMethodInfo<T>(this T source)
        {
            foreach (var type in typeof(T).GetInheritanceChain())
            {
                IEnumerable<(MethodInfo method, string description)> checkers = _checksByType[type];

                foreach (var checker in checkers)
                {
                    var result = checker.method.Invoke(null, new Object[] { source }) as string;

                    yield return (test: checker.description, result: result);
                }
            }
        }

        public static IEnumerable<(string test, string result)> CheckByDelegate<T>(this T source)
        {
            foreach (var type in typeof(T).GetInheritanceChain())
            {
                var checkers = _checkDelegatesByType[type];

                foreach (var checker in checkers)
                {
                    var method = checker.method as Func<T, string>;
                    var result = method(source);

                    yield return (test: checker.description, result: result);
                }
            }
        }


        private static IEnumerable<(MethodInfo method, string description)> _allCheckMethods()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var allTypes = assembly.GetTypes();
            var allMethods = allTypes.SelectMany(type => type.GetMethods());

            foreach (var method in allMethods)
            {
                // contains the Check attribute
                var hasAttribute = method.GetCustomAttributes<CheckAttribute>().Any();

                // is static
                var isStatic = method.IsStatic;

                // accepts a single parameter
                var hasSingleParameter = method.GetParameters().Length == 1;

                // returns string
                var returnsString = method.ReturnType == typeof(string);

                if (hasAttribute && isStatic && hasSingleParameter && returnsString)
                {
                    var attr = method.GetCustomAttribute<CheckAttribute>();
                    var description = attr.Description;

                    if (string.IsNullOrWhiteSpace(description)) description = method.Name;

                    yield return (method: method, description: description);
                }
            }
        }

        private static IEnumerable<(Delegate method, string description, Type type)> _allCheckDelegates()
        {
            foreach (var item in _allCheckMethods())
            {
                var methodInfo = item.method;
                var funcOpenType = typeof(Func<,>);
                var firstParamType = methodInfo.GetParameters()[0].ParameterType;

                var funcCloseType = funcOpenType.MakeGenericType(firstParamType, typeof(string));


                var dlgt = Delegate.CreateDelegate(funcCloseType, methodInfo);
                yield return (method: dlgt, description: item.description, type: methodInfo.GetParameters()[0].ParameterType);
            }
        }

        private static ILookup<Type, (MethodInfo method, string description)> _getChecksByType()
        {
            return _allCheckMethods().ToLookup(tpl => tpl.method.GetParameters()[0].ParameterType);
        }

        private static ILookup<Type, (Delegate method, string description)> _getCheckDelegatesByType()
        {
            return _allCheckDelegates().ToLookup(tpl => tpl.type, tpl => (method: tpl.method, description: tpl.description));
        }

    }
}
