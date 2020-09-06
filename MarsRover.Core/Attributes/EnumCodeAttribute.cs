using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumCodeAttribute : Attribute
    {
        public string Code { get; }

        public EnumCodeAttribute(string code)
        {
            Code = code;
        }
    }
}
