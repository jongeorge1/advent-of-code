namespace AdventOfCode.Year2023.Day09
{
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            IEnumerable<Sequence> sequences = input.Select(x => new Sequence(x));
            return sequences.Sum(x => x.ExtrapolateNextValue()).ToString();
        }

        private class Sequence
        {
            private int[] numbers;

            public Sequence(string input)
            {
                this.numbers = input.Split(' ').Select(int.Parse).ToArray();
            }

            public int ExtrapolateNextValue()
            {
                List<List<int>> differences = [this.numbers.ToList()];

                while (differences.Last().Any(x => x != 0))
                {
                    List<int> target = differences.Last();
                    List<int> result = [];
                    for (int i = 1; i < target.Count; ++i)
                    {
                        result.Add(target[i] - target[i - 1]);
                    }

                    differences.Add(result);
                }

                // Now extrapolate all the way up
                for (int i = differences.Count - 2; i >= 0; --i)
                {
                    differences[i].Add(differences[i].Last() + differences[i + 1].Last());
                }

                return differences[0].Last();
            }
        }
    }
}
