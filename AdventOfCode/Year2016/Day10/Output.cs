namespace AdventOfCode.Year2016.Day10
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("Bot {Number}")]
    public class Output : IDestination
    {
        public Output(int number)
        {
            this.Number = number;
            this.Values = [];
        }

        public int Number { get; }

        public List<int> Values { get; }
    }
}