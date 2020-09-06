
using MarsRover.Core.Enums;
using MarsRover.Core.Models; 

namespace MarsRover.Core.Services
{
    public interface IHeadingAppService
    {
        Location Move(Location location);
        Heading TurnRight();
        Heading TurnLeft();
    }
}
