namespace AdventOfCode.Year2016.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerDisplay("Bot {Number}")]
    public class Bot : IDestination
    {
        public Bot(int number)
        {
            this.Number = number;
            this.Values = new List<int>();
            this.LowDestination = null;
            this.HighDestination = null;
            this.Executed = false;
        }

        public int Number { get; }

        public List<int> Values { get; }

        public IDestination? LowDestination { get; set; }

        public IDestination? HighDestination { get; set; }

        public bool Executed { get; private set; }

        public bool HasValues(int low, int high)
        {
            return this.Values.Contains(low) && this.Values.Contains(high);
        }

        public bool CanExecute()
        {
            return this.Values.Count == 2 && this.LowDestination is not null && this.HighDestination is not null && !this.Executed;
        }

        public int LowValue()
        {
            return this.Values.Min();
        }

        public int HighValue()
        {
            return this.Values.Max();
        }

        public void Execute()
        {
            this.LowDestination!.Values.Add(this.LowValue());
            this.HighDestination!.Values.Add(this.HighValue());
            this.Executed = true;
        }
    }
}
