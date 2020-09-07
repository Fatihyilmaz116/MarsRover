using System;
using System.Collections.Generic;
using System.Text;
using MarsRover.Core.Models;
using MarsRover.Services.Platform;
using MarsRover.Services.Rover;
using MarsRover.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
namespace MarsRover.UI.Console
{
    public class Program
    {
        private static IPlatformAppService _platformAppService;
        private static IRoverAppService _roverAppService;

        public static Platform CurrentPlatform { get; set; }

        public static List<Rover> RoverList { get; set; }

        public static int RoverMaximumCount = 2;

        static void Main(string[] args)
        {
            //Dependency Injection
            var services = new ServiceCollection()
                    .AddSingleton<IPlatformAppService, PlatformAppService>()
                    .AddSingleton<IRoverAppService, RoverAppService>()
                    .BuildServiceProvider();

            _platformAppService = services.GetService<IPlatformAppService>();
            _roverAppService = services.GetService<IRoverAppService>();

          

            RoverList = new List<Rover>();

            System.Console.WriteLine("Please create a new platform");

            bool work = true;

            while(work)
            {
                StartPlatform();

                StartRover();

                Calculate();

                System.Console.WriteLine("Press any key for exit");

                System.Console.ReadLine(); 

                work = false;
            }

        }


        public static void StartPlatform()
        { 
          
            while (CurrentPlatform == null)
            {
                string platform = System.Console.ReadLine();

                CurrentPlatform = _platformAppService.Create(platform);

                if (CurrentPlatform == null)
                {
                    System.Console.WriteLine("Invalid coordinate, Example coordinate: 3 3");
                }
            }

        }

        public static void StartRover()
        {
            while (RoverList.Count != RoverMaximumCount)
            {
                bool roverIsReady = false;
                bool roverCommandsReady = false;

                System.Console.WriteLine($"Enter {RoverList.Count + 1}.Rover coordinate");

                if (!roverIsReady)
                {
                    var roverCoordinate = System.Console.ReadLine();

                    var roverCreateResult = _roverAppService.Create(roverCoordinate, CurrentPlatform);

                    var currentRover = roverCreateResult.Rover;

                    if (currentRover == null)
                    {
                        System.Console.WriteLine(roverCreateResult.AlertMessage);
                    }
                    else
                    {
                        roverIsReady = true;

                        System.Console.WriteLine($"Enter {RoverList.Count + 1}.Rover commands");

                        if (!roverCommandsReady)
                        { 
                            var roverCommands = _roverAppService.GetCommandList(System.Console.ReadLine());

                            if (roverCommands.Count == 0)
                            {
                                System.Console.WriteLine($"Please enter {RoverList.Count + 1}.Rover valid command values,Example: \"MLMLMLMML\"");
                            }
                            else
                            {
                                roverCommandsReady = true;

                                currentRover.Commands = roverCommands;

                                RoverList.Add(currentRover);
                            }
                        }
                    }
                }
            }
        }

        public static void Calculate()
        {  
            foreach (var rover in RoverList)
            { 
                _roverAppService.CalculateCommands(rover);

                System.Console.WriteLine($"{rover.Location.X} {rover.Location.Y} {rover.Location.Heading.GetEnumCode()}");
            }
        }




    }
}
