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
        public static int MaxRoverCount => 2;

        public static Platform CurrentPlatform { get; set; }

        public static List<Rover> RoverList { get; set; }

        static void Main(string[] args)
        {
            #region Dependency Injection

            var services = new ServiceCollection()
                    .AddSingleton<IPlatformAppService, PlatformAppService>()
                    .AddSingleton<IRoverAppService, RoverAppService>()
                    .BuildServiceProvider();

            _platformAppService = services.GetService<IPlatformAppService>();
            _roverAppService = services.GetService<IRoverAppService>();

            #endregion

            RoverList = new List<Rover>();

            System.Console.WriteLine("Please create a new platform");

            bool work = true;

            while(work)
            {
                StartPlatform();

                StartRover();

                ExecuteWorker();

                System.Console.WriteLine("Press any key for exit");

                System.Console.ReadLine(); 

                work = false;
            }

        }


        public static void StartPlatform()
        { 
          
            while (CurrentPlatform == null)
            {
                string platformCoordinate = System.Console.ReadLine();

                CurrentPlatform = _platformAppService.Create(platformCoordinate);

                if (CurrentPlatform == null)
                {
                    System.Console.WriteLine("Invalid coordinate, Example coordinate: \"3 3\"");
                }
            }

        }

        public static void StartRover()
        {
            while (RoverList.Count != MaxRoverCount)
            {
                bool roverIsReady = false;
                bool roverCommandsAreReady = false;

                System.Console.WriteLine($"Enter {RoverList.Count + 1}.Rover coordinate");

                while (!roverIsReady)
                {
                    var roverCoordinateString = System.Console.ReadLine();

                    var roverCreateResult = _roverAppService.Create(roverCoordinateString, CurrentPlatform);

                    var currentRover = roverCreateResult.Rover;

                    if (currentRover == null)
                    {
                        System.Console.WriteLine(roverCreateResult.AlertMessage);
                    }
                    else
                    {
                        roverIsReady = true;

                        System.Console.WriteLine($"Enter {RoverList.Count + 1}.Rover commands");

                        while (!roverCommandsAreReady)
                        {
                            var roverCommand = System.Console.ReadLine();

                            var roverCommands = _roverAppService.GetCommands(roverCommand);

                            if (roverCommands.Length == 0)
                            {
                                System.Console.WriteLine($"Please enter {RoverList.Count + 1}.Rover valid command values like this template: \"MLMLMLMML\"");
                            }
                            else
                            {
                                roverCommandsAreReady = true;

                                currentRover.Commands = roverCommands;

                                RoverList.Add(currentRover);
                            }
                        }
                    }
                }
            }
        }

        public static void ExecuteWorker()
        {
            var count = 0;

            foreach (var rover in RoverList)
            {
                count++;

                _roverAppService.ExecuteCommands(rover);

                System.Console.WriteLine($"{count}. Rover => {rover.Location.X} {rover.Location.Y} {rover.Location.Heading.GetEnumCode()}");
            }
        }




    }
}
