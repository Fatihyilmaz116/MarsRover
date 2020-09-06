using MarsRover.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text; 

namespace MarsRover.Core.Models
{
    public class Rover
    {
        public Location Location { get; set; }
        public Platform Platform { get; }

        public Command[] Commands { get; set; }
        public List<RoverHistory> RoverHistory { get; set; }

        public Rover(Location location, Platform _platform)
        {
            Location = location;
            Platform = _platform;
            RoverHistory = new List<RoverHistory>();
        }
    }
}
