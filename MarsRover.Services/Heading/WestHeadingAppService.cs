using MarsRover.Core.Models;
using MarsRover.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using EnumHeading = MarsRover.Core.Enums;
namespace MarsRover.Services.Heading
{
    public class WestHeadingAppService:IHeadingAppService
    {
        public Location Move(Location location)
        { 
            return new Location(location.X - 1, location.Y, location.Heading);
        }

        public EnumHeading.Heading TurnRight()
        {
            return EnumHeading.Heading.North;
        }

        public EnumHeading.Heading TurnLeft()
        {
            return EnumHeading.Heading.South;
        }
    }
}
