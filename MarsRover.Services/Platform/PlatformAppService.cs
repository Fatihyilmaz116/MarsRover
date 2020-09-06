using System;
using System.Collections.Generic;
using System.Text;
using MarsRover.Core.Models;
namespace MarsRover.Services.Platform
{
    public class PlatformAppService : IPlatformAppService
    {
        public Core.Models.Platform Create(string coordinate)
        {
           
            if (string.IsNullOrEmpty(coordinate) || string.IsNullOrWhiteSpace(coordinate))
            {
                return null;
            }

            var separateds = coordinate.Split(' ');

            if (separateds.Length != 2)
            {
                return null;
            }

            if (!int.TryParse(separateds[0], out var xCoordinate))
            {
                return null;
            }

            if (!int.TryParse(separateds[1], out var yCoordinate))
            {
                return null;
            }

            return new Core.Models.Platform(xCoordinate, yCoordinate);
        }
 
    }
}
