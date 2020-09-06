using MarsRover.Core.Enums;
using MarsRover.Core.Models; 

namespace MarsRover.Services.Rover
{
   public interface IRoverAppService
    {
        RoverResult Create(string coordinateString, MarsRover.Core.Models.Platform platform); 
        MarsRover.Core.Models.Rover ExecuteCommands(MarsRover.Core.Models.Rover rover);
        Command[] GetCommands(string command);
    }
}
