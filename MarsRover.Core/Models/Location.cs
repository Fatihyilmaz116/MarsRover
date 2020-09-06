using MarsRover.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text; 

namespace MarsRover.Core.Models
{
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Heading Heading { get; set; } 

        public Location(int x, int y, Heading _heading)
        {
            X = x;
            Y = y;
            Heading = _heading;
        }
    }
}
