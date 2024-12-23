﻿namespace AdventOfCode.Year2016.Day10
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Day10Common
    {
        private readonly Regex allocationRegex = new(@"value (\d+)[\w\s]+ (\d+)");
        private readonly Regex instructionRegex = new(@"bot (\d+) gives low to (\w+) (\d+) and high to (\w+) (\d+)");

        public Dictionary<int, Bot> Bots { get; } = [];

        public Dictionary<int, Output> Outputs { get; } = [];

        public void ProcessInput(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].StartsWith("value"))
                {
                    Match match = this.allocationRegex.Match(input[i]);
                    this.GetBot(int.Parse(match.Groups[2].Value)).Values.Add(int.Parse(match.Groups[1].Value));
                }
                else
                {
                    Match match = this.instructionRegex.Match(input[i]);
                    Bot bot = this.GetBot(int.Parse(match.Groups[1].Value));
                    bot.LowDestination = this.GetDestination(match.Groups[2].Value, int.Parse(match.Groups[3].Value));
                    bot.HighDestination = this.GetDestination(match.Groups[4].Value, int.Parse(match.Groups[5].Value));
                }
            }
        }

        public Bot GetBot(int number)
        {
            if (!this.Bots.TryGetValue(number, out Bot? bot))
            {
                bot = new Bot(number);
                this.Bots.Add(number, bot);
            }

            return bot;
        }

        public Output GetOutput(int number)
        {
            if (!this.Outputs.TryGetValue(number, out Output? output))
            {
                output = new(number);
                this.Outputs.Add(number, output);
            }

            return output;
        }

        private IDestination GetDestination(string type, int number)
        {
            return type == "bot" ? this.GetBot(number) : this.GetOutput(number);
        }
    }
}
