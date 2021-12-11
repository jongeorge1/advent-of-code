namespace AoC.Solutions.Year2016.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions.Helpers;

    public class Floor
    {
        private readonly int level;
        private List<string> chips;
        private List<string> generators;

        public Floor(int level, string input)
        {
            this.level = level;
            this.chips = new ();
            this.generators = new ();

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
            this.level = level;
            this.chips = new List<string>(chips);
            this.generators = new List<string>(generators);
        }

        public bool IsEmpty => this.chips.Count == 0 && this.generators.Count == 0;

        public bool IsStateValid
        {
            get
            {
                if (this.generators.Count == 0 || this.chips.Count == 0)
                {
                    return true;
                }

                // We know there's at least one generator here, so if there's a chip that
                // doesn't match it then it'll get fried.
                return this.chips.All(chip => this.generators.Contains(chip));
            }
        }

        public Floor Clone()
        {
            return new Floor(this.level, this.chips.AsReadOnly(), this.generators.AsReadOnly());
        }

        public void AddChips(IEnumerable<string> chips)
        {
            this.chips.AddRange(chips);
            this.chips.Sort();
        }

        public void RemoveChips(IEnumerable<string> chips)
        {
            foreach (string current in chips)
            {
                this.chips.Remove(current);
            }
        }

        public void RemoveGenerators(IEnumerable<string> generators)
        {
            foreach (string current in generators)
            {
                this.generators.Remove(current);
            }
        }

        public void AddGenerators(IEnumerable<string> generators)
        {
            this.generators.AddRange(generators);
            this.generators.Sort();
        }

        public string Serialize() => $"{this.level}|{string.Join(',', this.generators)}|{string.Join(',', this.chips)}";

        public MovableCombination[] GetAllMovableCombinations()
        {
            var combos = new List<MovableCombination>();

            combos.AddRange(this.chips.Select(x => new MovableCombination(new[] { x }, System.Array.Empty<string>())));
            combos.AddRange(this.generators.Select(x => new MovableCombination(System.Array.Empty<string>(), new[] { x })));

            // Now all pairs of chips and all pairs of generators
            combos.AddRange(this.chips.GetPermutations(2).Select(x => new MovableCombination(x, System.Array.Empty<string>())));
            combos.AddRange(this.generators.GetPermutations(2).Select(x => new MovableCombination(System.Array.Empty<string>(), x)));

            // Finally all pairs of chip + generator
            foreach (string chip in this.chips)
            {
                combos.AddRange(this.generators.Select(g => new MovableCombination(new[] { chip }, new[] { g })));
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
                this.generators.Add(element);
            }
            else
            {
                this.chips.Add(element);
            }
        }
    }
}
