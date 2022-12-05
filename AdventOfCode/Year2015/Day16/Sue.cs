namespace AdventOfCode.Year2015.Day16
{
    using System;
    using System.Collections.Generic;

    public class Sue
    {
        public Sue(string input)
        {
            string[] components = input.Split(new[] { ' ', ',', ':' }, StringSplitOptions.RemoveEmptyEntries);
            this.Number = components[1];

            int index = 2;
            while (index < components.Length)
            {
                this.Information.Add(components[index], int.Parse(components[index + 1]));
                index += 2;
            }
        }

        public string Number { get; }

        public Dictionary<string, int> Information { get; set; } = new Dictionary<string, int>();

        public bool Matches(Dictionary<string, int> facts)
        {
            foreach (KeyValuePair<string, int> fact in facts)
            {
                if (this.Information.TryGetValue(fact.Key, out int factValue) && factValue != fact.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Matches2(Dictionary<string, int> facts)
        {
            foreach (KeyValuePair<string, int> fact in facts)
            {
                if (this.Information.TryGetValue(fact.Key, out int factValue))
                {
                    switch (fact.Key)
                    {
                        case "cats":
                        case "trees":
                            if (factValue <= fact.Value)
                            {
                                return false;
                            }

                            break;

                        case "pomeranians":
                        case "goldfish":
                            if (factValue >= fact.Value)
                            {
                                return false;
                            }

                            break;

                        default:
                            if (factValue != fact.Value)
                            {
                                return false;
                            }

                            break;
                    }
                }
            }

            return true;
        }
    }
}
