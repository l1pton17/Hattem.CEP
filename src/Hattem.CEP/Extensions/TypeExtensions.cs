using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hattem.CEP.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsAssignableFromGenericInterface(this Type interfaceType, Type type)
        {
            return GetImplementedGenericInterfaceTypes(interfaceType, type).Any();
        }

        private static IEnumerable<Type> GetImplementedGenericInterfaceTypes(this Type interfaceType, Type type)
        {
            return type
                .GetTypeInfo()
                .GetInterfaces()
                .Where(
                    v => v.GetTypeInfo().IsGenericType && v.GetGenericTypeDefinition() == interfaceType);
        }
    }
}
