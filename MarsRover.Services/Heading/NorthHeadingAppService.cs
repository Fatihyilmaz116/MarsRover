using MarsRover.Core.Models;
using MarsRover.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using EnumHeading = MarsRover.Core.Enums;

namespace MarsRover.Services.Heading
{
    public class NorthHeadingAppService:IHeadingAppService
    {
        public Location Move(Location location)
        {   
            return new Location(location.X, location.Y + 1, location.Heading);
        }

        public EnumHeading.Heading TurnRight()
        {
            return EnumHeading.Heading.East;
        }

        public EnumHeading.Heading TurnLeft()
        {
            return EnumHeading.Heading.West;
        }
    }
}
