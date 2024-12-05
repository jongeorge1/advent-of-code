namespace AdventOfCode.Year2022.Day10
{
    using System;

    public class TickEventArgs : EventArgs
    {
        public int Cycle { get; set; }

        public static TickEventArgs Create(int cycle)
        {
            return new TickEventArgs
            {
                Cycle = cycle,
            };
        }
    }
}
