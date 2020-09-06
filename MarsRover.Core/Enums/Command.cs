using MarsRover.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.Enums
{
    public enum Command
    {
        [EnumCode("M")]
        Forward = 1,

        [EnumCode("L")]
        Left = 2,

        [EnumCode("R")]
        Right = 3
    }
}
