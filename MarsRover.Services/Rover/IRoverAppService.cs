using MarsRover.Core.Enums;
using MarsRover.Core.Models;
using System.Collections.Generic;

namespace MarsRover.Services.Rover
{
   public interface IRoverAppService
    {
        RoverResult Create(string coordinate, Core.Models.Platform platform); 
        MarsRover.Core.Models.Rover CalculateCommands(Core.Models.Rover rover);
        List<Command> GetCommandList(string command);
    }
}
