namespace AdventOfCode.Year2021.Day04
{
    using System;
    using System.Linq;

    public class Board
    {
        private int[] board;
        private bool[] marks;
        private int width;
        private int height;

        public Board(string input)
        {
            int[][] rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
                .ToArray();

            this.width = rows[0].Length;
            this.height = rows.Length;
            this.board = rows.SelectMany(x => x).ToArray();
            this.marks = new bool[this.board.Length];
        }

        public int Score(int draw)
        {
            return this.marks.Select((m, i) => m ? 0 : this.board[i]).Sum() * draw;
        }

        public void Mark(int number)
        {
            int index = Array.IndexOf(this.board, number);
            if (index != -1)
            {
                this.marks[index] = true;
            }
        }

        public bool HasWon()
        {
            // Look for a complete row
            for (int row = 0; row < this.height; ++row)
            {
                int rowStart = row * this.width;
                if (this.marks[rowStart..(rowStart + this.width)].All(x => x))
                {
                    return true;
                }
            }

            // Look for a complete column
            for (int column = 0; column < this.width; ++column)
            {
                for (int row = 0; row < this.height; ++row)
                {
                    if (!this.marks[(row * this.width) + column])
                    {
                        goto COLUMN_HASNT_WON;
                    }
                }

                return true;

            COLUMN_HASNT_WON:
                ;
            }

            return false;
        }
    }
}
