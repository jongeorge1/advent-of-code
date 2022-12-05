namespace AdventOfCode.Year2016.Day11
{
    using System.Collections.Generic;
    using System.Linq;

    public class MovableCombination
    {
        public MovableCombination(IEnumerable<string> chips, IEnumerable<string> generators)
        {
            this.Chips = chips.OrderBy(x => x).ToList();
            this.Generators = generators.OrderBy(x => x).ToList();

            this.Id = string.Concat("chips:", string.Join(',', this.Chips), "/generators:", string.Join(',', this.Generators));
        }

        public string Id { get; }

        public List<string> Chips { get; }

        public List<string> Generators { get; }
    }
}
