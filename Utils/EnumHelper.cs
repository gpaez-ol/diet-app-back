using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AlgoFit.Utils.Enums
{
    public static class EnumHelper
    {
        public static string GetEnumText(Enum value)
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttribute.Value;
        }
    }
}
