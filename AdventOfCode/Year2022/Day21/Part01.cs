namespace AdventOfCode.Year2022.Day21
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            Dictionary<string, Monkey> monkeys = new();

            foreach (string line in input)
            {
                string[] elements = line.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (elements.Length == 4)
                {
                    var newMonkey = new OperatorMonkey
                    {
                        Name = elements[0],
                        Left = elements[1],
                        Right = elements[3],
                        Operator = elements[2][0],
                    };

                    monkeys.Add(newMonkey.Name, newMonkey);
                }
                else if (elements.Length == 2)
                {
                    var newMonkey = new NumberMonkey
                    {
                        Name = elements[0],
                        Number = long.Parse(elements[1]),
                    };

                    monkeys.Add(newMonkey.Name, newMonkey);
                }
                else
                {
                    throw new Exception();
                }
            }

            Monkey rootMonkey = monkeys["root"];
            long result = rootMonkey.Yell(ref monkeys);
            return result.ToString();
        }
    }

    public abstract class Monkey
    {
        protected Monkey()
        {
        }

        public string Name { get; set; }

        public abstract long Yell(ref Dictionary<string, Monkey> allMonkeys);
    }

    public class NumberMonkey : Monkey
    {
        public long Number { get; set; }

        public override long Yell(ref Dictionary<string, Monkey> allMonkeys) => this.Number;
    }

    public class OperatorMonkey : Monkey
    {
        private static readonly Dictionary<char, Func<long, long, long>> OperatorFunctions = new Dictionary<char, Func<long, long, long>>
        {
            { '+', (a, b) => a + b },
            { '-', (a, b) => a - b },
            { '*', (a, b) => a * b },
            { '/', (a, b) => a / b },
        };

        public string Left { get; set; }

        public string Right { get; set; }

        public char Operator { get; set; }

        public override long Yell(ref Dictionary<string, Monkey> allMonkeys)
        {
            long left = allMonkeys[this.Left].Yell(ref allMonkeys);
            long right = allMonkeys[this.Right].Yell(ref allMonkeys);
            return OperatorFunctions[this.Operator](left, right);
        }
    }
}
