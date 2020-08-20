using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckPersonToConsoleTheSlowWay();
        }

        static void InvestigateTypePerson()
        {
            GenericHelper.LogCaller();

            var person = new Person
            {
                FullName = "John Smith",
                Age = 42,
                BirthCountry = "England",
                Profession = "Teacher",
                Father = new Person { FullName = "Mark Smith" },
                Mother = new Person { FullName = "Jane Smith" },
                Children = new List<Person> {
                    new Person { FullName = "Paul Smith" },
                    new Person { FullName = "George Smith"}
                }
            };

            var personType = person.GetType();

            Console.WriteLine($"Type Name: {personType.Name}");
            Console.WriteLine($"Type Full Name: {personType.FullName}");
            Console.WriteLine($"Assembly: {personType.Assembly.FullName}");

            var properties = personType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Properties");
            foreach (var prop in properties)
            {
                Console.WriteLine($"Name: {prop.Name}, Type: {prop.PropertyType.FullGenericString()}");
            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Methods");
            var methods = personType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            foreach (var method in methods)
            {
                Console.Write($"Name: {method.Name}, Return Type: {method.ReturnType.Name}");
                var parameters = method.GetParameters();

                var paramStrings = parameters.Select(prm => $"{prm.Name}: {prm.ParameterType.Name}");
                var prmString = $"({string.Join(", ", paramStrings)})";
                Console.WriteLine(prmString);
            }
        }

        static void ReflectionInvokation()
        {
            var personType = typeof(Person);

            var person = Activator.CreateInstance(personType);

            var fullNameProperty = personType.GetProperty("FullName", BindingFlags.Public | BindingFlags.Instance);
            fullNameProperty.SetValue(person, "John Smith");

            var genderProperty = personType.GetProperty("Gender", BindingFlags.Public | BindingFlags.Instance);
            genderProperty.SetValue(person, Gender.Male);

            var getTitleMethod = personType.GetMethod("GetTitle");
            var title = getTitleMethod.Invoke(person, new object[0]) as string;
        }

        static void InvestigateGenerics()
        {
            // closed Type
            var listIntType = typeof(List<int>);

            // open type
            var listTType = typeof(List<>);

            // create close type from open type (generic type definition)
            var listStringType = listTType.MakeGenericType(typeof(string));


        }

        static void TestListGenerator()
        {
            var person = new Person { FullName = "John Smith" };
            var list = person.WrapInList();
        }

        static void CheckPersonToConsoleTheSlowWay()
        {
            var person = new Person
            {
                FullName = "John Smith",
                Age = 42,
            };

            person.CheckByDelegate().ToList();
            person.CheckByMethodInfo().ToList();

            var timesToRun = 10000;

            var start = DateTime.Now;
            for (int i = 0; i < timesToRun; i++)
            {
                var report = person.CheckByMethodInfo().ToList();
            }
            var timer1 = DateTime.Now - start;

            start = DateTime.Now;
            for (int i = 0; i < timesToRun; i++)
            {
                var report = person.CheckByDelegate().ToList();
            }
            var timer2 = DateTime.Now - start;

            Console.WriteLine($"{timesToRun} checks by reflection invokation: {timer1}");
            Console.WriteLine($"{timesToRun} checks by delegate invokation: {timer2}");


            Console.ReadLine();
        }
    }
}
