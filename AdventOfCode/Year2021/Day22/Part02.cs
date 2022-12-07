namespace AdventOfCode.Year2021.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var reactor = new Reactor();

            foreach (string current in rows)
            {
                string[] instruction = current.Split(new char[] { ' ', '=', '.', ',' }, StringSplitOptions.RemoveEmptyEntries);

                bool on = instruction[0] == "on";

                var newRegion = new Region(int.Parse(instruction[2]), int.Parse(instruction[3]), int.Parse(instruction[5]), int.Parse(instruction[6]), int.Parse(instruction[8]), int.Parse(instruction[9]));

                reactor.SwitchRegion(newRegion, on);
            }

            return reactor.ActiveCubeCount().ToString();
        }
    }
}
