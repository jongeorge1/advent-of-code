namespace AoC.Solutions.Year2021.Day04
{
    using System;
    using System.Linq;
    using AoC.Solutions;

    public static class BoardExtensions
    {
        public static void MarkAll(this Board[] boards, int number)
        {
            foreach (Board board in boards)
            {
                board.Mark(number);
            }
        }

        public static Board? GetWinner(this Board[] boards)
        {
            return boards.SingleOrDefault(x => x.HasWon());
        }

        public static Board[] GetLosers(this Board[] boards)
        {
            return boards.Where(x => !x.HasWon()).ToArray();
        }
    }
}
