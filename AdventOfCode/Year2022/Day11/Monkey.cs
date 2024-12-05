namespace AdventOfCode.Year2022.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Monkey
    {
        private static readonly Func<long, long> DefaultStressManagementStrategy = input => input / 3;

        public Monkey(string configuration, Func<long, long>? stressManagementStrategy = null)
        {
            string[] configurationRows = configuration.Split(Environment.NewLine);

            this.Number = configurationRows[0][7] - '0';

            this.Items = configurationRows[1].Split(new[] { ' ', ':', ',' }, StringSplitOptions.RemoveEmptyEntries)[2..].Select(long.Parse).ToList();

            string[] operationSegments = configurationRows[2][19..].Split(' ');

            // TODO: operation thing
            this.Operation = operationSegments switch
            {
            ["old", "*", "old"] => (item) => item * item,
            ["old", "*", _] => (item) => item * int.Parse(operationSegments[2]),
            ["old", "+", _] => (item) => item + int.Parse(operationSegments[2]),
                _ => throw new Exception("Unexpected operation definition"),
            };

            // Test is always "divisible by <something>"
            this.TestDivisor = int.Parse(configurationRows[3][21..]);

            this.ThrowTargets = new Dictionary<bool, int>
            {
                { true, int.Parse(configurationRows[4][29..]) },
                { false, int.Parse(configurationRows[5][30..]) },
            };

            this.StressManagementStrategy = stressManagementStrategy ?? DefaultStressManagementStrategy;
        }

        public int Number { get; }

        public List<long> Items { get; }

        public Func<long, long> Operation { get; }

        public int TestDivisor { get; }

        public Dictionary<bool, int> ThrowTargets { get; }

        public Func<long, long> StressManagementStrategy { get; set; }

        public long ItemsInspected { get; private set; } = 0;

        public void InspectAndThrowAllItems(Monkey[] allMonkeys)
        {
            foreach (long item in this.Items)
            {
                long worryLevelAfterInspection = this.StressManagementStrategy(this.Operation(item));
                bool inspectionResult = worryLevelAfterInspection % this.TestDivisor == 0;
                int targetMonkey = this.ThrowTargets[inspectionResult];
                allMonkeys[targetMonkey].Items.Add(worryLevelAfterInspection);
            }

            this.ItemsInspected += this.Items.Count;
            this.Items.Clear();
        }
    }
}
