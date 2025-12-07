namespace AdventOfCode.Year2023.Day19;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Channels;
using AdventOfCode;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        Dictionary<string, Workflow> workflows = ParseInput(input);

        return string.Empty;
    }

    private static Dictionary<string, Workflow> ParseInput(string[] input)
    {
        Dictionary<string, Workflow> workflows = [];

        foreach (string row in input)
        {
            if (row == string.Empty)
            {
                return workflows;
            }

            Workflow workflow = new(row);
            workflows.Add(workflow.Id, workflow);
        }

        Debug.Fail("Reached supposedly unreachable code");
        return [];
    }

    ////private static object DoSomeWork(Workflow workflow, PartRanges inputRanges)
    ////{

    ////}

    [DebuggerDisplay("{Definition}")]
    private record Workflow
    {
        public Workflow(string definition)
        {
            string[] components = definition.Split(['{', ':', ',', '}'], StringSplitOptions.RemoveEmptyEntries);
            this.Definition = definition;
            this.Id = components[0];
            List<Rule> rules = [];

            // Rules are in pairs, except the last one.
            for (int index = 1; index < components.Length - 1; index += 2)
            {
                Func<int, int, PartRanges> rangeBuilder = PartRanges.Builders[components[index][0]];
                int value = int.Parse(components[index][2..]);

                PartRanges ruleRanges = components[index][1] == '<'
                    ? rangeBuilder(1, value - 1)
                    : rangeBuilder(value + 1, 4000);

                rules.Add(new(ruleRanges, components[index + 1]));
            }

            // Now we should be left with the final rule
            rules.Add(new(PartRanges.Full, components[^1]));

            this.Rules = [.. rules];
        }

        public string Definition { get; }

        public string Id { get; }

        public ImmutableArray<Rule> Rules { get; }
    }

    private record Rule(PartRanges Ranges, string Outcome)
    {
        ////public (PartRanges InRange, PartRanges OutRange) ApplyRange(PartRanges input)
        ////{

        ////}
    }

    private record PartRanges(int XMin, int XMax, int MMin, int MMax, int AMin, int AMax, int SMin, int SMax)
    {
        public static readonly PartRanges Full = new(1, 4000, 1, 4000, 1, 4000, 1, 4000);

        public static readonly Dictionary<char, Func<int, int, PartRanges>> Builders = new()
        {
            { 'x', (min, max) => new PartRanges(min, max, 1, 4000, 1, 4000, 1, 4000) },
            { 'm', (min, max) => new PartRanges(1, 4000, min, max, 1, 4000, 1, 4000) },
            { 'a', (min, max) => new PartRanges(1, 4000, 1, 4000, min, max, 1, 4000) },
            { 's', (min, max) => new PartRanges(1, 4000, 1, 4000, 1, 4000, min, max) },
        };

        public PartRanges Intersect(PartRanges other)
        {
            return new(
                Math.Max(this.XMin, other.XMin),
                Math.Min(this.XMax, other.XMax),
                Math.Max(this.MMin, other.MMin),
                Math.Min(this.MMax, other.MMax),
                Math.Max(this.AMin, other.AMin),
                Math.Min(this.AMax, other.AMax),
                Math.Max(this.SMin, other.SMin),
                Math.Min(this.SMax, other.SMax));
        }
    }
}
