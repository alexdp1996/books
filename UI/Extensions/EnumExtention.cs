using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace UI.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
                where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }

        public static string GetDisplay(this Enum enumValue)
        {
            return GetAttribute<DisplayAttribute>(enumValue).Name;
        }
    }
}