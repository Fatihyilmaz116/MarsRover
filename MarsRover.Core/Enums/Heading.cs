using MarsRover.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.Enums
{  
    public enum Heading
    {
        [EnumCode("N/A")]
        NA = 0,

        [EnumCode("N")]
        North = 1,

        [EnumCode("S")]
        South = 2,

        [EnumCode("E")]
        East = 3,

        [EnumCode("W")]
        West = 4
    }

}
