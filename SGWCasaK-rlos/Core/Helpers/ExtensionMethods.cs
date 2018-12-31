using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Helpers
{
    public static class ExtensionMethods
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetTypeInfo().GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        public static string GetDescription(this Enum value)
        {
            return value.GetAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
        }
    }
}
