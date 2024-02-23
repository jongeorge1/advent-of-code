namespace AdventOfCode.Year2023.Day14
{
    using System;
    using System.Runtime.CompilerServices;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            PlatformItem[,] layout = BuildPlatformLayout(input);

            for (int i = 0; i < 1000000; i++)
            {
                layout = TiltNorth(layout);
                layout = TiltWest(layout);
                layout = TiltSouth(layout);
                layout = TiltEast(layout);
            }

            Console.WriteLine($"After 1000000 cycles:");
            PrintLayout(layout);
            Console.WriteLine();

            int score = CalculateLoad(layout);

            return score.ToString();
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

        public enum PlatformItem : byte
        {
            Empty = 0,
            RoundRock = 1,
            CubeRock = 2,
        }
    }
}
