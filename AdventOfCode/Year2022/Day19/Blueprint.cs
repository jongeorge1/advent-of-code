namespace AdventOfCode.Year2022.Day19
{
    using System;
    using AdventOfCode.Helpers;

    public readonly record struct Blueprint(
        byte Number,
        byte OreRobotOreCost,
        byte ClayRobotOreCost,
        byte ObsidianRobotOreCost,
        byte ObsidianRobotClayCost,
        byte GeodeRobotOreCost,
        byte GeodeRobotObsidianCost,
        byte MaximumRequiredOreRobots,
        byte MaximumRequiredClayRobots,
        byte MaximumRequiredObsidianRobots)
    {
        public static Blueprint FromInputString(ReadOnlySpan<char> input)
        {
            var wordEnumerator = StringExtensions.OptimizedSplit(input, " ".AsSpan());
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            byte number = byte.Parse(wordEnumerator.Current.Line[..^1]);
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            byte oreRobotOreCost = byte.Parse(wordEnumerator.Current.Line);
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            byte clayRobotOreCost = byte.Parse(wordEnumerator.Current.Line);
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            byte obsidianRobotOreCost = byte.Parse(wordEnumerator.Current.Line);
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            byte obsidianRobotClayCost = byte.Parse(wordEnumerator.Current.Line);
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            byte geodeRobotOreCost = byte.Parse(wordEnumerator.Current.Line);
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            wordEnumerator.MoveNext();
            byte geodeRobotObsidianCost = byte.Parse(wordEnumerator.Current.Line);

            byte maximumRequiredOreRobots = Math.Max(
                Math.Max(oreRobotOreCost, clayRobotOreCost),
                Math.Max(obsidianRobotOreCost, geodeRobotOreCost));

            return new Blueprint(
                number,
                oreRobotOreCost,
                clayRobotOreCost,
                obsidianRobotOreCost,
                obsidianRobotClayCost,
                geodeRobotOreCost,
                geodeRobotObsidianCost,
                maximumRequiredOreRobots,
                obsidianRobotClayCost,
                geodeRobotObsidianCost);
        }
    }
}
