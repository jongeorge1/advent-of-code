namespace AdventOfCode.Year2015.Day13
{
    using System.Collections.Generic;

    public class Person
    {
        public Person(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public Dictionary<string, int> HappinessRules { get; set; } = new Dictionary<string, int>();

        public int CalculateHappinessUnits(string left, string right)
        {
            int total = 0;

            if (this.HappinessRules.TryGetValue(left, out int leftVal))
            {
                total += leftVal;
            }

            if (this.HappinessRules.TryGetValue(right, out int rightVal))
            {
                total += rightVal;
            }

            return total;
        }
    }
}
