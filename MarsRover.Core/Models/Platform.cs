using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.Models
{
    public class Platform
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Platform(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
