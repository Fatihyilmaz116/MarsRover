using MarsRover.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MarsRover.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumCode(this Enum @enum)
        {
            var attribute = @enum.GetType()
                .GetMember(@enum.ToString())
                .First()
                .GetCustomAttribute<EnumCodeAttribute>();

            return attribute.Code;
        }
    }
}
