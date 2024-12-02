namespace AdventOfCode.Year2023.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public enum PlatformItem : byte
        {
            Empty = 0,
            RoundRock = 1,
            CubeRock = 2,
        }

        public string Solve(string[] input)
        {
            PlatformItem[,] layout = BuildPlatformLayout(input);

            Dictionary<int, int> seenStates = new();

            int cycles = 0;

            // First, loop until we hit a state we've seen before.

            int interval = 0;
            int firstOccurance = 0;

            do
            {
                layout = Cycle(layout);

                int hash = GetLayoutHash(layout);

                ++cycles;

                // Have we seen this has before?
                if (seenStates.TryGetValue(hash, out firstOccurance))
                {
                    // Work out how many more we need to do as a result.
                    interval = cycles - firstOccurance;
                }

                seenStates[hash] = cycles;
            }
            while (interval == 0);

            Console.WriteLine($"First occurance {firstOccurance}, interval {interval}");

            // Use these to work out how many more cycles are required before we stop.
            int remainingCycles = (1000000000 - firstOccurance) % interval;

            for (int i = 0; i < remainingCycles; ++i)
            {
                layout = Cycle(layout);
            }

            int score = CalculateLoad(layout);

            return score.ToString();
        }

        private static PlatformItem[,] Cycle(PlatformItem[,] layout)
        {
            layout = TiltNorth(layout);
            layout = TiltWest(layout);
            layout = TiltSouth(layout);
            layout = TiltEast(layout);
            return layout;
        }

        private static int GetLayoutHash(PlatformItem[,] layout)
        {
            StringBuilder builder = new();
            for (int row = 0; row < layout.GetLength(1); ++row)
            {
                for (int col = 0; col < layout.GetLength(0); ++col)
                {
                    builder.Append((byte)layout[col, row]);
                }
            }

            return builder.ToString().GetHashCode();
        }

        private static void PrintLayout(PlatformItem[,] layout)
        {
            int width = layout.GetLength(0);
            int height = layout.GetLength(1);

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    char output = layout[x, y] switch
                    {
                        PlatformItem.Empty => '.',
                        PlatformItem.RoundRock => 'O',
                        PlatformItem.CubeRock => '#',
                        _ => throw new Exception("Unexpected layout item"),
                    };
                    Console.Write(output);
                }

                Console.WriteLine();
            }
        }

        private static PlatformItem[,] BuildPlatformLayout(string[] input)
        {
            var layout = new PlatformItem[input[0].Length, input.Length];

            for (int row = 0; row < input.Length; ++row)
            {
                for (int col = 0; col < input[0].Length; ++col)
                {
                    layout[col, row] = input[row][col] switch
                    {
                        '#' => PlatformItem.CubeRock,
                        'O' => PlatformItem.RoundRock,
                        _ => PlatformItem.Empty,
                    };
                }
            }

            return layout;
        }

        private static int CalculateLoad(PlatformItem[,] layout)
        {
            int width = layout.GetLength(0);
            int height = layout.GetLength(1);

            int load = 0;

            for (int row = 0; row < height; ++row)
            {
                int rowLoad = height - row;

                for (int col = 0; col < width; ++col)
                {
                    if (layout[col, row] == PlatformItem.RoundRock)
                    {
                        load += rowLoad;
                    }
                }
            }

            return load;
        }

        private static PlatformItem[,] TiltNorth(PlatformItem[,] current)
        {
            int width = current.GetLength(0);
            int height = current.GetLength(1);

            var tilted = new PlatformItem[width, height];

            for (int col = 0; col < width; ++col)
            {
                int firstEmptyRow = 0;

                for (int row = 0; row < height; ++row)
                {
                    switch (current[col, row])
                    {
                        case PlatformItem.Empty:
                            break;

                        case PlatformItem.CubeRock:
                            tilted[col, row] = PlatformItem.CubeRock;
                            firstEmptyRow = row + 1;
                            break;

                        case PlatformItem.RoundRock:
                            if (firstEmptyRow < row)
                            {
                                tilted[col, firstEmptyRow] = PlatformItem.RoundRock;
                                ++firstEmptyRow;
                            }
                            else
                            {
                                tilted[col, row] = PlatformItem.RoundRock;
                                firstEmptyRow = row + 1;
                            }

                            break;
                    }
                }
            }

            return tilted;
        }

        private static PlatformItem[,] TiltWest(PlatformItem[,] current)
        {
            int width = current.GetLength(0);
            int height = current.GetLength(1);

            var tilted = new PlatformItem[width, height];

            for (int row = 0; row < height; ++row)
            {
                int firstEmptyCol = 0;

                for (int col = 0; col < width; ++col)
                {
                    switch (current[col, row])
                    {
                        case PlatformItem.Empty:
                            break;

                        case PlatformItem.CubeRock:
                            tilted[col, row] = PlatformItem.CubeRock;
                            firstEmptyCol = col + 1;
                            break;

                        case PlatformItem.RoundRock:
                            if (firstEmptyCol < col)
                            {
                                tilted[firstEmptyCol, row] = PlatformItem.RoundRock;
                                ++firstEmptyCol;
                            }
                            else
                            {
                                tilted[col, row] = PlatformItem.RoundRock;
                                firstEmptyCol = col + 1;
                            }

                            break;
                    }
                }
            }

            return tilted;
        }

        private static PlatformItem[,] TiltSouth(PlatformItem[,] current)
        {
            int width = current.GetLength(0);
            int height = current.GetLength(1);

            var tilted = new PlatformItem[width, height];

            for (int col = 0; col < width; ++col)
            {
                int firstEmptyRow = height - 1;

                for (int row = height - 1; row >= 0; --row)
                {
                    switch (current[col, row])
                    {
                        case PlatformItem.Empty:
                            break;

                        case PlatformItem.CubeRock:
                            tilted[col, row] = PlatformItem.CubeRock;
                            firstEmptyRow = row - 1;
                            break;

                        case PlatformItem.RoundRock:
                            if (firstEmptyRow > row)
                            {
                                tilted[col, firstEmptyRow] = PlatformItem.RoundRock;
                                --firstEmptyRow;
                            }
                            else
                            {
                                tilted[col, row] = PlatformItem.RoundRock;
                                firstEmptyRow = row - 1;
                            }

                            break;
                    }
                }
            }

            return tilted;
        }

        private static PlatformItem[,] TiltEast(PlatformItem[,] current)
        {
            int width = current.GetLength(0);
            int height = current.GetLength(1);

            var tilted = new PlatformItem[width, height];

            for (int row = 0; row < height; ++row)
            {
                int firstEmptyCol = width - 1;

                for (int col = width - 1; col >= 0; --col)
                {
                    switch (current[col, row])
                    {
                        case PlatformItem.Empty:
                            break;

                        case PlatformItem.CubeRock:
                            tilted[col, row] = PlatformItem.CubeRock;
                            firstEmptyCol = col - 1;
                            break;

                        case PlatformItem.RoundRock:
                            if (firstEmptyCol > col)
                            {
                                tilted[firstEmptyCol, row] = PlatformItem.RoundRock;
                                --firstEmptyCol;
                            }
                            else
                            {
                                tilted[col, row] = PlatformItem.RoundRock;
                                firstEmptyCol = col - 1;
                            }

                            break;
                    }
                }
            }

            return tilted;
        }
    }
}
