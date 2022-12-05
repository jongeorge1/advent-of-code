namespace AdventOfCode.Year2015.Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            Dictionary<string, Person> people = BuildPeopleDictionary(input);
            people.Add("Me", new Person("Me"));

            string[][] arrangements = people.Keys.GetPermutations().Select(x => x.ToArray()).ToArray();

            int bestScore = arrangements.Max(arr => ScoreArrangement(arr, people));

            return bestScore.ToString();
        }

        private static int ScoreArrangement(string[] arrangement, Dictionary<string, Person> people)
        {
            int total = 0;

            for (int seat = 0; seat < arrangement.Length; seat++)
            {
                int left = seat == 0 ? arrangement.Length - 1 : seat - 1;
                int right = (seat + 1) % arrangement.Length;

                total += people[arrangement[seat]].CalculateHappinessUnits(arrangement[left], arrangement[right]);
            }

            return total;
        }

        private static Dictionary<string, Person> BuildPeopleDictionary(string input)
        {
            var result = new Dictionary<string, Person>();

            foreach (string rule in input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] components = rule.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string name = components[0];

                if (!result.TryGetValue(name, out Person? person))
                {
                    person = new Person(name);
                    result.Add(person.Name, person);
                }

                int unitChange = int.Parse(components[3]);
                if (components[2] == "lose")
                {
                    unitChange = -unitChange;
                }

                person.HappinessRules.Add(components[10], unitChange);
            }

            return result;
        }
    }
}
