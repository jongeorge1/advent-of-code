namespace AdventOfCode.Year2022.Day11
{
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            Monkey[] monkeys = input.Select(x => new Monkey(x)).ToArray();

            for (int round = 0; round < 20; ++round)
            {
                foreach (Monkey monkey in monkeys)
                {
                    monkey.InspectAndThrowAllItems(monkeys);
                }
            }

            return monkeys
                .OrderByDescending(x => x.ItemsInspected)
                .Take(2)
                .Aggregate(1L, (prev, curr) => prev * curr.ItemsInspected).ToString();
        }
    }
}
