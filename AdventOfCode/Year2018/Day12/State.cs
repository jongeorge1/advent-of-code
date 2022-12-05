namespace AdventOfCode.Year2018.Day12
{
    using System.Collections.Generic;
    using System.Linq;

    public class State
    {
        public State(List<int> potsContainingPlants) => this.PotsContainingPlants = potsContainingPlants;

        public List<int> PotsContainingPlants { get; set; }

        public static State Parse(string input)
        {
            return new State(Enumerable.Range(0, input.Length).Where(x => input[x] == '#').ToList());
        }

        public override string ToString()
        {
            return string.Join(",", this.PotsContainingPlants.OrderBy(x => x));
        }
    }
}
