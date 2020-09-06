using MarsRover.Core.Models;
using MarsRover.Core.Services; 
using EnumHeading = MarsRover.Core.Enums;
namespace MarsRover.Services.Heading
{
    public class EastHeadingAppService : IHeadingAppService
    {
        public Location Move(Location location)
        {   
            return new Location(location.X + 1, location.Y, location.Heading);
        }
            

        public EnumHeading.Heading TurnRight()
        {
            return EnumHeading.Heading.South;
        }

        public EnumHeading.Heading TurnLeft()
        {
            return EnumHeading.Heading.North;
        }


    }
}
