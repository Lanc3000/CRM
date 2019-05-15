using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CRMCore.Helpers
{
    public static class EnumHelper
    {
        public static T[] GetValues<T>()
        {
            return (T[])System.Enum.GetValues(typeof(T));
        }

        public static EnumValue<T>[] GetLocalizedValues<T>()
        {
            return GetValues<T>()
                .Select(v => new EnumValue<T>()
                {
                    Value = v,
                    DisplayName = DisplayName(v)
                }).ToArray();
        }

        public static string DisplayName<T>(T value)
        {
            var displayName = GetDisplayValue(value);
            return displayName;
        }

        private static string GetDisplayValue<T>(T value)
        {
            var type = value.GetType();
            var memInfo = type.GetRuntimeField(value.ToString());
            if (memInfo == null)
            {
                return string.Empty;
            }
            var attr = memInfo.GetCustomAttribute<DisplayAttribute>();
            return attr.Name;
        }
    }

    public class EnumValue<T>
    {
        public string DisplayName { get; set; }

        public T Value { get; set; }
    }
}
