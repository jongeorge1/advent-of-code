namespace AdventOfCode.Year2020.Day03
{
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            return (this.CountTreesOnSlope(input, 1, 1)
                * this.CountTreesOnSlope(input, 3, 1)
                * this.CountTreesOnSlope(input, 5, 1)
                * this.CountTreesOnSlope(input, 7, 1)
                * this.CountTreesOnSlope(input, 1, 2)).ToString();
        }

        private int CountTreesOnSlope(string[] map, int colStep, int rowStep)
        {
            int rowCount = map.Length;
            int colCount = map[0].Length;

            int treeCount = 0;
            int currentRow = 0;
            int currentCol = 0;

            do
            {
                if (map[currentRow][currentCol % colCount] == '#')
                {
                    treeCount++;
                }

                currentRow += rowStep;
                currentCol += colStep;
            }
            while (currentRow < rowCount);

            return treeCount;
        }
    }
}
