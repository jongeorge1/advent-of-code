namespace AdventOfCode.Year2022.Day21
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        private static readonly Dictionary<char, Func<long, long, long>> OperatorInversionsForKnownLeftSide = new Dictionary<char, Func<long, long, long>>
        {
            { '+', (long knownValue, long target) => target - knownValue },
            { '-', (long knownValue, long target) => knownValue - target },
            { '*', (long knownValue, long target) => target / knownValue },
            { '/', (long knownValue, long target) => knownValue / target },
        };

        private static readonly Dictionary<char, Func<long, long, long>> OperatorInversionsForKnownRightSide = new Dictionary<char, Func<long, long, long>>
        {
            { '+', (long knownValue, long target) => target - knownValue },
            { '-', (long knownValue, long target) => knownValue + target },
            { '*', (long knownValue, long target) => target / knownValue },
            { '/', (long knownValue, long target) => knownValue * target },
        };

        public string Solve(string[] input)
        {
            var monkeys = new Dictionary<string, Monkey>();

            var humanMonkey = new Human(ref monkeys);
            monkeys.Add("humn", humanMonkey);

            foreach (string line in input)
            {
                string[] elements = line.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (elements[0] == "humn")
                {
                    continue;
                }

                if (elements.Length == 4)
                {
                    var newMonkey = new OperatorMonkey(ref monkeys)
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
                    var newMonkey = new NumberMonkey(ref monkeys)
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

            var rootMonkey = (OperatorMonkey)monkeys["root"];

            // Collapse down branches that don't rely on humans
            Collapse(rootMonkey, ref monkeys);

            // This will leave us with one side of the root monkey's comparison that's a number, and the other that's a big tree of stuff
            // ending with a human on one side and a number on the other.
            // This means we know what our target is and we can apply the operations in reverse as we head down the stack until we reach
            // the human.
            long currentTarget = 0;
            Monkey current;
            if (rootMonkey.Left is NumberMonkey)
            {
                currentTarget = rootMonkey.LeftMonkey.Yell();
                current = rootMonkey.RightMonkey;
            }
            else
            {
                currentTarget = rootMonkey.RightMonkey.Yell();
                current = rootMonkey.LeftMonkey;
            }

            while (!(current is Human))
            {
                // One side should be a NumberMonkey and the other side either another OperatorMonkey or the human.
                // Which side the NumberMonkey is matters in some cases.
                var currentOperatorMonkey = (OperatorMonkey)current;
                if (currentOperatorMonkey.LeftMonkey is NumberMonkey)
                {
                    currentTarget = OperatorInversionsForKnownLeftSide[currentOperatorMonkey.Operator](currentOperatorMonkey.LeftMonkey.Yell(), currentTarget);
                    current = currentOperatorMonkey.RightMonkey;
                }
                else
                {
                    currentTarget = OperatorInversionsForKnownRightSide[currentOperatorMonkey.Operator](currentOperatorMonkey.RightMonkey.Yell(), currentTarget);
                    current = currentOperatorMonkey.LeftMonkey;
                }
            }

            // Now current should be human, and we should have our number.
            return currentTarget.ToString();
        }

        private static void Collapse(OperatorMonkey start, ref Dictionary<string, Monkey> allMonkeys)
        {
            if (!start.DependsOnHuman)
            {
                allMonkeys[start.Name] = new NumberMonkey(ref allMonkeys)
                {
                    Name = start.Name,
                    Number = start.Yell(),
                };
            }

            var next = allMonkeys[start.Left] as OperatorMonkey;
            if (next is not null)
            {
                Collapse(next, ref allMonkeys);
            }

            next = allMonkeys[start.Right] as OperatorMonkey;
            if (next is not null)
            {
                Collapse(next, ref allMonkeys);
            }
        }

        public abstract class Monkey
        {
            private bool? dependsOnHuman = null;

            protected Monkey(ref Dictionary<string, Monkey> allMonkeys)
            {
                this.AllMonkeys = allMonkeys;
            }

            public string? Name { get; set; }

            public bool DependsOnHuman
            {
                get
                {
                    if (!this.dependsOnHuman.HasValue)
                    {
                        this.dependsOnHuman = this.DetermineIfDependsOnHuman();
                    }

                    return this.dependsOnHuman.Value;
                }
            }

            protected Dictionary<string, Monkey> AllMonkeys { get; }

            public abstract long Yell();

            protected abstract bool DetermineIfDependsOnHuman();
        }

        public class Human : Monkey
        {
            public Human(ref Dictionary<string, Monkey> allMonkeys)
                : base(ref allMonkeys)
            {
                this.Name = "humn";
            }

            public override long Yell() => throw new Exception("I don't know what to say");

            protected override bool DetermineIfDependsOnHuman() => true;
        }

        public class NumberMonkey : Monkey
        {
            public NumberMonkey(ref Dictionary<string, Monkey> allMonkeys)
                : base(ref allMonkeys)
            {
            }

            public long Number { get; set; }

            public override long Yell() => this.Number;

            protected override bool DetermineIfDependsOnHuman() => false;
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

            private string? left;
            private string? right;

            public OperatorMonkey(ref Dictionary<string, Monkey> allMonkeys)
                : base(ref allMonkeys)
            {
            }

            public string Left
            {
                get => this.left ?? throw new InvalidOperationException();
                set => this.left = value;
            }

            public string Right
            {
                get => this.right ?? throw new InvalidOperationException();
                set => this.right = value;
            }

            public Monkey LeftMonkey => this.AllMonkeys[this.Left];

            public Monkey RightMonkey => this.AllMonkeys[this.Right];

            public char Operator { get; set; }

            public override long Yell()
            {
                long left = this.AllMonkeys[this.Left].Yell();
                long right = this.AllMonkeys[this.Right].Yell();
                return OperatorFunctions[this.Operator](left, right);
            }

            protected override bool DetermineIfDependsOnHuman() => this.AllMonkeys[this.Left].DependsOnHuman || this.AllMonkeys[this.Right].DependsOnHuman;
        }
    }
}