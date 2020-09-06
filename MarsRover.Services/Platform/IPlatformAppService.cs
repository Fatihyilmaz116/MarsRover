using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Services.Platform
{
    public interface IPlatformAppService
    {
        MarsRover.Core.Models.Platform Create(string coordinate); 
    }
}
