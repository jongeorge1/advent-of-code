namespace AdventOfCode.Year2022.Day11
{
    using System.Linq;
    using System;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            // For round 2, the numbers stack up too fast to even store in a long; we quickly end up with overflow issues.
            // To deal with that we need a stress management strategy that will not affect the tests.
            // Each test is a "divisble by..." test with the divisor being a prime. So we need a method of reducing the
            // input number without affecting whether or not it's divisible by any of those primes.
            // To do this, we need to find the lowest common multiple of all the divisors, Then our stress management
            // strategy can be taking the modulo of the input by that number...
            Monkey[] monkeys = input.Split(Environment.NewLine + Environment.NewLine).Select(x => new Monkey(x)).ToArray();

            long lowestCommonMultipleOfAllDivisors = Numeric.LeastCommonMultiple(monkeys.Select(x => (long)x.TestDivisor));
            Func<long, long> stressManagementStrategy = input => input % lowestCommonMultipleOfAllDivisors;
            foreach (Monkey monkey in monkeys)
            {
                monkey.StressManagementStrategy = stressManagementStrategy;
            }

            for (int round = 0; round < 10000; ++round)
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
