using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace HackathonCCR.EDM.Enums
{
    public static class Helper
    {
        public static string GetDescription(Enum @enum)
        {
            var type = @enum.GetType();
            var memInfo = type.GetMember(@enum.ToString());

            if (memInfo.Length <= 0)
                return @enum.ToString();

            var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attrs.Length > 0 ? ((DescriptionAttribute)attrs[0]).Description : @enum.ToString();
        }

        public static List<TEnum> GetEnumsFlags<TEnum>(this TEnum currentEnum, bool indexed = false) where TEnum : Enum
        {
            var result = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Where(f => currentEnum.HasFlag(f))
            .OrderBy(f => f.ToString())
            .ToList();

            return result;
        }

        public static string StringValueOf(Enum objEnum)
        {
            FieldInfo info = objEnum.GetType().GetField(objEnum.ToString());
            DescriptionAttribute[] atributos =
                (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (atributos.Length > 0)
                return atributos[0].Description;
            else
                return objEnum.ToString();
        }

        public static TEnum ListEnumToEnumFlag<TEnum>(this IEnumerable<TEnum> FromListOfEnums) where TEnum : Enum, IConvertible, IFormattable
        {
            var provider = new System.Globalization.NumberFormatInfo();
            var intlist = FromListOfEnums.Select(x => x.ToInt32(provider));
            var aggregatedint = intlist.Aggregate((prev, next) => prev | next);
            return (TEnum)Enum.ToObject(typeof(TEnum), aggregatedint);
        }

        public static List<TEnum> EnumFlagToListEnum<TEnum>(this TEnum currentEnum, bool indexed = false) where TEnum : Enum
        {
            var result = System.Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Where(f => currentEnum.HasFlag(f))
            .ToList();

            return result;
        }

        public static T ParseEnum<T>(string value)
        {
            if (!Enum.IsDefined(typeof(T), value))
                return default(T);

            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
