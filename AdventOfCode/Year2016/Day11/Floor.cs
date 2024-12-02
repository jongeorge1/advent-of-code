namespace AdventOfCode.Year2016.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Helpers;

    public class Floor
    {
        public Floor(int level, string input)
        {
            this.Level = level;
            this.Chips = new();
            this.Generators = new();

            if (!string.IsNullOrEmpty(input))
            {
                if (input.IndexOf("relevant.") != -1)
                {
                    return;
                }

                string[] components = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (string el in components)
                {
                    this.ParseInputComponent(el);
                }
            }
        }

        public Floor(int level, IEnumerable<string> chips, IEnumerable<string> generators)
        {
            this.Level = level;
            this.Chips = new List<string>(chips);
            this.Generators = new List<string>(generators);
        }

        public int Level { get; }

        public List<string> Chips { get; }

        public List<string> Generators { get; }

        public bool IsEmpty => this.Chips.Count == 0 && this.Generators.Count == 0;

        public bool IsStateValid
        {
            get
            {
                if (this.Generators.Count == 0 || this.Chips.Count == 0)
                {
                    return true;
                }

                // We know there's at least one generator here, so if there's a chip that
                // doesn't match it then it'll get fried.
                return this.Chips.All(chip => this.Generators.Contains(chip));
            }
        }

        public Floor Clone()
        {
            return new Floor(this.Level, this.Chips.AsReadOnly(), this.Generators.AsReadOnly());
        }

        public void AddChips(IEnumerable<string> chips)
        {
            this.Chips.AddRange(chips);
            this.Chips.Sort();
        }

        public void RemoveChips(IEnumerable<string> chips)
        {
            foreach (string current in chips)
            {
                this.Chips.Remove(current);
            }
        }

        public void RemoveGenerators(IEnumerable<string> generators)
        {
            foreach (string current in generators)
            {
                this.Generators.Remove(current);
            }
        }

        public void AddGenerators(IEnumerable<string> generators)
        {
            this.Generators.AddRange(generators);
            this.Generators.Sort();
        }

        public MovableCombination[] GetAllMovableCombinations()
        {
            var combos = new List<MovableCombination>();

            combos.AddRange(this.Chips.Select(x => new MovableCombination(new[] { x }, System.Array.Empty<string>())));
            combos.AddRange(this.Generators.Select(x => new MovableCombination(System.Array.Empty<string>(), new[] { x })));

            // Now all pairs of chips and all pairs of generators
            combos.AddRange(this.Chips.GetPermutations(2).Select(x => new MovableCombination(x, System.Array.Empty<string>())));
            combos.AddRange(this.Generators.GetPermutations(2).Select(x => new MovableCombination(System.Array.Empty<string>(), x)));

            // Finally all pairs of chip + generator
            foreach (string chip in this.Chips)
            {
                combos.AddRange(this.Generators.Select(g => new MovableCombination(new[] { chip }, new[] { g })));
            }

            // Finally, filter them down
            return combos.GroupBy(c => c.Id).Select(x => x.First()).ToArray();
        }

        private void ParseInputComponent(string input)
        {
            string[] components = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string element = components[^2].Replace("-compatible", string.Empty);
            string device = components[^1].Replace(".", string.Empty);

            if (device == "generator")
            {
                this.Generators.Add(element);
            }
            else
            {
                this.Chips.Add(element);
            }
        }
    }
}
