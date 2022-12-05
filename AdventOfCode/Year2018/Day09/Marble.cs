namespace AdventOfCode.Year2018.Day09
{
    using System.Diagnostics;

    [DebuggerDisplay("{Number} : {CounterClockwise.Number} - {Number} - {Clockwise.Number}")]
    public class Marble
    {
        public int Number { get; set; }

        public Marble? Clockwise { get; set; }

        public Marble? CounterClockwise { get; set; }
    }
}
