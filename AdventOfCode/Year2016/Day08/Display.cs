namespace AdventOfCode.Year2016.Day08
{
    using System;
    using System.Linq;

    public class Display
    {
        private bool[][] matrix;
        private int height;
        private int width;

        public Display(int height, int width)
        {
            this.height = height;
            this.width = width;
            this.matrix = new bool[height][];

            for (int row = 0; row < height; ++row)
            {
                this.matrix[row] = new bool[width];
            }
        }

        public void RotateRow(int y, int count)
        {
            bool[] row = this.matrix[y];
            bool[] newRow = new bool[this.width];

            for (int i = 0; i < this.width; ++i)
            {
                newRow[i] = row[(i - count + this.width) % this.width];
            }

            this.matrix[y] = newRow;
        }

        public void RotateColumn(int x, int count)
        {
            // Manky solution, repeatedly rotate by 1
            for (int i = 0; i < count; i++)
            {
                bool last = this.matrix[this.height - 1][x];

                for (int j = this.height - 1; j > 0; --j)
                {
                    this.matrix[j][x] = this.matrix[j - 1][x];
                }

                this.matrix[0][x] = last;
            }
        }

        public void Rect(int x, int y)
        {
            for (int i = 0; i < y; ++i)
            {
                for (int j = 0; j < x; ++j)
                {
                    this.matrix[i][j] = true;
                }
            }
        }

        public int CountLeds()
        {
            return this.matrix.Sum(x => x.Count(y => y));
        }

        public string DrawDisplay()
        {
            return string.Join(Environment.NewLine, this.matrix.Select(row => new string(row.Select(c => c ? '#' : '.').ToArray())));
        }
    }
}
