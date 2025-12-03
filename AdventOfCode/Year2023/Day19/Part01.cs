namespace AdventOfCode.Year2023.Day19;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        (List<Part> parts, Dictionary<string, Workflow> workflows) = ParseInput(input);

        Workflow start = workflows["in"];
        int total = 0;

        foreach (Part part in parts)
        {
            Console.Write(part.Definition);
            Console.Write(": ");

            Workflow? current = start;
            Console.Write(current.Id);

            while (current is not null)
            {
                string result = current.Apply(part);

                Console.Write(" -> ");
                Console.Write(result);

                if (result == "A")
                {
                    current = null;
                    total += part.Sum;
                }
                else if (result == "R")
                {
                    current = null;
                }
                else
                {
                    current = workflows[result];
                }
            }

            Console.WriteLine();
        }

        return total.ToString();
    }

    private static (List<Part> Parts, Dictionary<string, Workflow> Workflows) ParseInput(string[] input)
    {
        List<Part> parts = [];
        Dictionary<string, Workflow> workflows = [];

        bool readingWorkflows = true;

        Console.WriteLine("Reading workflows");

        foreach (string row in input)
        {
            if (row == string.Empty)
            {
                readingWorkflows = false;

                Console.WriteLine("Reading parts");

                continue;
            }

            if (readingWorkflows)
            {
                Workflow workflow = new(row);
                workflows.Add(workflow.Id, workflow);
            }
            else
            {
                parts.Add(new Part(row));
            }
        }

        Console.WriteLine($"Read {workflows.Count} workflows and {parts.Count} parts");
        Console.WriteLine();

        return (parts, workflows);
    }

    [DebuggerDisplay("{Definition}")]
    private record Part
    {
        public Part(string definition)
        {
            string[] components = definition.Split(['{', 'x', 'm', 'a', 's', '=', ',', '}'], StringSplitOptions.RemoveEmptyEntries);
            this.Definition = definition;
            this.X = int.Parse(components[0]);
            this.M = int.Parse(components[1]);
            this.A = int.Parse(components[2]);
            this.S = int.Parse(components[3]);
            this.Sum = this.X + this.M + this.A + this.S;
        }

        public string Definition { get; }

        public int X { get; }

        public int M { get; }

        public int A { get; }

        public int S { get; }

        public int Sum { get; }
    }

    [DebuggerDisplay("{Definition}")]
    private record Workflow
    {
        private static readonly ParameterExpression Parameter = Expression.Parameter(typeof(Part), "part");
        private static readonly Dictionary<char, Expression> Properties = new()
        {
            { 'x', Expression.Property(Parameter, "X") },
            { 'm', Expression.Property(Parameter, "M") },
            { 'a', Expression.Property(Parameter, "A") },
            { 's', Expression.Property(Parameter, "S") },
        };

        private static readonly Func<Part, bool> NullTest = _ => true;

        public Workflow(string definition)
        {
            string[] components = definition.Split(['{', ':', ',', '}'], StringSplitOptions.RemoveEmptyEntries);
            this.Definition = definition;
            this.Id = components[0];
            List<Rule> rules = [];

            // Rules are in pairs, except the last one.
            for (int index = 1; index < components.Length - 1; index += 2)
            {
                Expression property = Properties[components[index][0]];
                Expression constant = Expression.Constant(int.Parse(components[index][2..]));

                Expression rule = components[index][1] == '<'
                    ? Expression.LessThan(property, constant)
                    : Expression.GreaterThan(property, constant);

                Func<Part, bool> test = Expression.Lambda<Func<Part, bool>>(rule, Parameter).Compile();

                rules.Add(new(test, components[index + 1]));
            }

            // Now we should be left with the final rule
            rules.Add(new(NullTest, components[^1]));

            this.Rules = [.. rules];
        }

        public string Definition { get; }

        public string Id { get; }

        public ImmutableArray<Rule> Rules { get; }

        public string Apply(Part part)
        {
            foreach (Rule rule in this.Rules)
            {
                if (rule.Test(part))
                {
                    return rule.Outcome;
                }
            }

            Debug.Fail("Something went wrong with rule evalutation");

            return string.Empty;
        }
    }

    private record Rule(Func<Part, bool> Test, string Outcome)
    {
    }
}
