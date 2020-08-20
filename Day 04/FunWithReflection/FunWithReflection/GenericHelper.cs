using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    public static class GenericHelper
    {
        /// <summary>
        /// Uses reflection to create an instance of List<type>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ToList(this Type type)
        {
            var genericListType = typeof(List<>);

            var closedType = genericListType.MakeGenericType(type);
            var instance = Activator.CreateInstance(closedType);
            return instance;
        }

        public static object WrapInList(this object item)
        {
            var typeOfItem = item.GetType();
            var list = typeOfItem.ToList();

            var addMethod = list.GetType().GetMethod("Add");
            addMethod.Invoke(list, new object[] { item });

            return list;
        }

        public static string FullGenericString(this Type type)
        {
            if (!type.IsGenericType) return type.Name;

            if (type.IsGenericTypeDefinition) return $"{type.Name}<>";

            var typeParameters = type.GetGenericArguments();

            var typeParemeterNames = typeParameters.Select(typeParameter => typeParameter.FullGenericString());

            return $"{type.Name}<{string.Join(", ", typeParemeterNames)}>";

        }
    }
}
