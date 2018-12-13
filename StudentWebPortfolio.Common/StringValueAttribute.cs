using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StudentWebPortfolio.Common
{
    public class StringValueAttribute : Attribute
    {
        public string Value { get; private set; }

        public StringValueAttribute(string value)
        {
            this.Value = value;
        }
    }

    public static class StringValueAttributeExtension
    {
        public static string GetStringValue(this Enum element)
        {
            var type = element.GetType();
            var field = type.GetRuntimeField(element.ToString());
            return field != null ?
                (field.GetCustomAttributes(typeof(StringValueAttribute), false).FirstOrDefault() as StringValueAttribute)?.Value
                : null;
        }

        public static EnumType ParseEnumByStringValue<EnumType>(this string value, EnumType defaultValue = default)
        {
            var result = Enum.GetValues(typeof(EnumType)).Cast<EnumType>().Where(_ => GetStringValue(_ as Enum) == value);
            return result.Any() ? result.First() : defaultValue;
        }
    }
}
