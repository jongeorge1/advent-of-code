namespace AdventOfCode.Year2022.Day22
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        private const char OpenTile = '.';
        private const char SolidWall = '#';
        private const char Void = ' ';
        private static readonly char[] Directions = ['L', 'R'];

        private static readonly (int DX, int DY)[] DirectionOffsets = new[]
        {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1),
        };

        public string Solve(string[] input)
        {
            ReadOnlySpan<char> instructions = input[^1].AsSpan();

            input = input[..^2];

            // To make things easier, we're going to maintain a list of start and end
            // indices for each row and column of the map
            (int Start, int End)[] mapXBounds = Enumerable.Range(0, input.Length).Select(_ => (int.MaxValue, 0)).ToArray();
            (int Start, int End)[] mapYBounds = Enumerable.Range(0, input.Max(row => row.Length)).Select(_ => (int.MaxValue, 0)).ToArray();

            for (int row = 0; row < mapXBounds.Length; ++row)
            {
                for (int col = 0; col < mapYBounds.Length; ++col)
                {
                    if (col < input[row].Length && input[row][col] != Void)
                    {
                        if (row < mapYBounds[col].Start)
                        {
                            mapYBounds[col].Start = row;
                        }
                        else if (row > mapYBounds[col].End)
                        {
                            mapYBounds[col].End = row;
                        }

                        if (col < mapXBounds[row].Start)
                        {
                            mapXBounds[row].Start = col;
                        }

                        if (col > mapXBounds[row].End)
                        {
                            mapXBounds[row].End = col;
                        }
                    }
                }
            }

            (int X, int Y) location = (mapXBounds[0].Start, 0);
            int direction = 0;

            while (instructions.Length > 0)
            {
                // The next thing we want to consume will be a number
                int nextDirectionChange = instructions.IndexOfAny(Directions);

                int number = nextDirectionChange == -1
                    ? int.Parse(instructions)
                    : int.Parse(instructions[0..nextDirectionChange]);

                // Apply the steps
                for (int i = 0; i < number; ++i)
                {
                    // Get the next destination in the current direction.
                    (int X, int Y) destination = (location.X + DirectionOffsets[direction].DX, location.Y + DirectionOffsets[direction].DY);

                    // Handle wrapping.
                    if (direction == 0 || direction == 2)
                    {
                        // Check the x
                        if (destination.X < mapXBounds[destination.Y].Start)
                        {
                            destination.X = mapXBounds[destination.Y].End;
                        }
                        else if (destination.X > mapXBounds[destination.Y].End)
                        {
                            destination.X = mapXBounds[destination.Y].Start;
                        }
                    }
                    else
                    {
                        // Check the y
                        if (destination.Y < mapYBounds[destination.X].Start)
                        {
                            destination.Y = mapYBounds[destination.X].End;
                        }
                        else if (destination.Y > mapYBounds[destination.X].End)
                        {
                            destination.Y = mapYBounds[destination.X].Start;
                        }
                    }

                    // If our move wouldn't take us into a wall, go ahead
                    if (input[destination.Y][destination.X] == OpenTile)
                    {
                        location = destination;
                    }
                    else
                    {
                        break;
                    }
                }

                // Change direction
                if (nextDirectionChange != -1)
                {
                    char newDirection = instructions[nextDirectionChange];
                    direction = newDirection == 'R' ? ++direction : --direction;
                    direction = (direction + 4) % 4;
                }

                // Move to the next instruction
                instructions = nextDirectionChange == -1
                    ? ReadOnlySpan<char>.Empty
                    : instructions[(nextDirectionChange + 1)..];
            }

            int password = ((location.X + 1) * 4) + ((location.Y + 1) * 1000) + direction;
            return password.ToString();
        }
    }
}
