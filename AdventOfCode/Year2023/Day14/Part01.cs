namespace AdventOfCode.Year2023.Day14
{
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int runningTotal = 0;

            // Column by column...
            for (int column = 0; column < input[0].Length; column++)
            {
                // Row by row
                int firstEmptyRow = 0;
                int currentRow = 0;

                while (currentRow < input.Length)
                {
                    // What's in the current row?
                    switch (input[currentRow][column])
                    {
                        case '.':
                            // Nothing of interest. Crack on.
                            break;

                        case '#':
                            // A cube-shaped rock. This means we need to update our "first empty row" as rocks
                            // cannot come past this point.
                            firstEmptyRow = currentRow + 1;

                            break;

                        case 'O':
                            // This rock might be able to roll back...
                            if (firstEmptyRow < currentRow)
                            {
                                // It can roll. It will go as far as the first empty row and stop, so we know
                                // how much load it will contribute...
                                runningTotal += input.Length - firstEmptyRow;
                                ++firstEmptyRow;
                            }
                            else
                            {
                                // It can't roll, so it must stay where it is. Just score it as-is.
                                runningTotal += input.Length - currentRow;
                                firstEmptyRow = currentRow + 1;
                            }

                            break;
                    }

                    ++currentRow;
                }

            }

            return runningTotal.ToString();
        }
    }
}
