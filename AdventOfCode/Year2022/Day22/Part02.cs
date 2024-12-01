namespace AdventOfCode.Year2022.Day22
{
    using System;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        private static readonly char[] Directions = new[] { 'L', 'R' };

        public string Solve(string[] input)
        {
            ReadOnlySpan<char> instructions = input[^1].AsSpan();

            input = input[..^2];

            var cubeMap = new CubeMap(input);

            // We'll be starting at the top left of the first cube.
            CubeFaceDescriptor currentCube = cubeMap.CubeFaces[0];
            (int X, int Y) currentLocation = (currentCube.MapGridXBounds.Start, currentCube.MapGridYBounds.Start);
            int currentDirection = 0;

            // Console.WriteLine($"Starting at ({currentLocation.X}, {currentLocation.Y}) on face {currentCube.Id} facing {currentDirection}");

            while (instructions.Length > 0)
            {
                // The next thing we want to consume will be a number
                int nextDirectionChange = instructions.IndexOfAny(Directions);

                int number = nextDirectionChange == -1
                    ? int.Parse(instructions)
                    : int.Parse(instructions[0..nextDirectionChange]);

                // Console.WriteLine($"Attempting to move {number} steps");

                // Apply the steps
                for (int i = 0; i < number; ++i)
                {
                    // Get the next destination in the current direction.
                    ((int X, int Y) proposedLocation, CubeFaceDescriptor proposedLocationCubeFace, int proposedNewDirection) = currentCube.GetNextLocationFrom(currentLocation, currentDirection);

                    if (input[proposedLocation.Y][proposedLocation.X] == CubeMap.OpenTile)
                    {
                        // Console.WriteLine($"Moving to ({proposedLocation.X}, {proposedLocation.Y}) on face {proposedLocationCubeFace.Id} facing {proposedNewDirection}");

                        currentCube = proposedLocationCubeFace;
                        currentLocation = proposedLocation;
                        currentDirection = proposedNewDirection;
                    }
                    else
                    {
                        // Console.WriteLine($"Cannot take next step to ({proposedLocation.X}, {proposedLocation.Y}) on face {proposedLocationCubeFace.Id} facing {proposedNewDirection}; moving to next instruction");
                        break;
                    }
                }

                // Change direction
                if (nextDirectionChange != -1)
                {
                    char newDirection = instructions[nextDirectionChange];
                    currentDirection = newDirection == 'R' ? ++currentDirection : --currentDirection;
                    currentDirection = (currentDirection + 4) % 4;

                    // Console.WriteLine($"Changing direction to {currentDirection}");
                }

                // Move to the next instruction
                instructions = nextDirectionChange == -1
                    ? ReadOnlySpan<char>.Empty
                    : instructions[(nextDirectionChange + 1)..];
            }

            int password = ((currentLocation.X + 1) * 4) + ((currentLocation.Y + 1) * 1000) + currentDirection;
            return password.ToString();
        }

    }
}
