using System;
using System.Linq;
using MarsRover.Core.Enums;
using MarsRover.Core.Extensions;
using MarsRover.Core.Models;
using MarsRover.Core.Services;
using MarsRover.Services.Heading;
using MarsRover.Services.Rover;
using System.Collections.Generic;

public class RoverAppService : IRoverAppService
{

    public RoverResult Create(string coordinate, Platform platform)
    {
        RoverResult roverResult = new RoverResult();

        if (platform == null)
        {
            roverResult.AlertMessage = "Platform is empty";

            return roverResult;
        }

        var locationResult = CreateRoverLocation(platform, coordinate);

        if (locationResult.Location == null)
        {
            roverResult.AlertMessage = locationResult.AlertMessage;

            return roverResult;
        }

        roverResult.Rover = new Rover(locationResult.Location, platform);

        return roverResult;
    }


    public Rover ExecuteCommands(Rover rover)
    {
        int count = 0;

        foreach (var command in rover.Commands)
        {
            count++;

            bool roverIsInPlatform = CheckRoverIsInPlatform(rover);

            if (!roverIsInPlatform)
            {
                break;
            }

            if (rover.RoverHistory.Count == 5)
            {

            }

            Execute(rover, command);

            rover.RoverHistory.Add(new RoverHistory
            {
                Step = count,
                Location = rover.Location
            });
        }

        return rover;
    }

    public Command[] GetCommands(string command)
    {
        Command[] validCommands =
            Enum.GetValues(typeof(Command))
                .Cast<Command>()
                .ToArray();

        string[] commandCodes = validCommands.Select(x => x.GetEnumCode()).ToArray();

        string[] separatedCommands = command.ToCharArray().Select(x => x.ToString()).ToArray();

        string[] unsupportedCommands = separatedCommands.Distinct().Except(commandCodes).ToArray();

        if (unsupportedCommands.Length > 0)
        {
            return new Command[0];
        }

        List<Command> commands = new List<Command>();

        foreach (var commandItem in separatedCommands)
        {
            Command commandEnum = validCommands.First(x => x.GetEnumCode() == commandItem);

            commands.Add(commandEnum);
        }

        return commands.ToArray();
    }


    private void Execute(Rover rover, Command commandType)
    {
        IHeadingAppService _headingService;

        if (rover.Location.Heading == Heading.North)
        {
            _headingService = new NorthHeadingAppService();
        }
        else if (rover.Location.Heading == Heading.South)
        {
            _headingService = new SouthHeadingAppService();
        }
        else if (rover.Location.Heading == Heading.East)
        {
            _headingService = new EastHeadingAppService();
        }
        else if (rover.Location.Heading == Heading.West)
        {
            _headingService = new WestHeadingAppService();
        }
        else
        {
            _headingService = null;
        }


        if (_headingService != null)
        {
            switch (commandType)
            {
                case Command.Forward:
                    {
                        rover.Location = _headingService.Move(rover.Location);
                        break;
                    }

                case Command.Left:
                    {
                        rover.Location.Heading = _headingService.TurnLeft();
                        break;
                    }

                case Command.Right:
                    {
                        rover.Location.Heading = _headingService.TurnRight();
                        break;
                    }
            }
        }

    }


    private bool CheckRoverIsInPlatform(Rover rover)
    {
        double maxXCoordinateValue = rover.Platform.X;

        double maxYCoordinateValue = rover.Platform.Y;

        bool roverIsInPlatform = true;

        if (rover.Location.X > maxXCoordinateValue || rover.Location.Y > maxYCoordinateValue)
        {
            roverIsInPlatform = false;
        }
        else if (rover.Location.X < 0 || rover.Location.Y < 0)
        {
            roverIsInPlatform = false;
        }

        return roverIsInPlatform;
    }

    private LocationResult CreateRoverLocation(Platform platform, string coordinateString)
    {
        LocationResult locationResult = new LocationResult();

        if (string.IsNullOrEmpty(coordinateString) || string.IsNullOrWhiteSpace(coordinateString))
        {
            locationResult.AlertMessage = "Coordinate string must be this format: 1 2 N";

            return locationResult;
        }

        string[] separatedValues = coordinateString.Split(' ');

        if (separatedValues.Length != 3)
        {
            locationResult.AlertMessage = "Coordinate string must be includes x,y and heading value";

            return locationResult;
        }

        if (!int.TryParse(separatedValues[0], out var xCoordinate))
        {
            locationResult.AlertMessage = "Coordinate x is not valid";

            return locationResult;
        }

        if (!int.TryParse(separatedValues[1], out var yCoordinate))
        {
            locationResult.AlertMessage = "Coordinate y is not valid";

            return locationResult;
        }

        if (xCoordinate < 0 || xCoordinate > platform.X)
        {
            locationResult.AlertMessage = "Coordinate x is out plateau";

            return locationResult;
        }

        if (yCoordinate < 0 || yCoordinate > platform.Y)
        {
            locationResult.AlertMessage = "Coordinate y is out plateau";

            return locationResult;
        }

        Heading? heading = CreateHeading(separatedValues[2]);

        if (heading == null)
        {
            locationResult.AlertMessage = "Heading is not valid (N-S-E-W)";

            return locationResult;
        }

        locationResult.Location = new Location(xCoordinate, yCoordinate, heading.Value);

        return locationResult;
    }

    private Heading? CreateHeading(string headingCode)
    {
        Heading[] headings = Enum.GetValues(typeof(Heading)).Cast<Heading>().ToArray();

        Heading heading = headings.FirstOrDefault(x => x.GetEnumCode() == headingCode);

        return heading == Heading.NA ? (Heading?)null : (Heading?)heading;
    }

}

