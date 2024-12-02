namespace AdventOfCode.Year2021.Day22
{
    using System;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            var reactor = new Reactor();

            foreach (string current in input)
            {
                string[] instruction = current.Split(new char[] { ' ', '=', '.', ',' }, StringSplitOptions.RemoveEmptyEntries);

                bool on = instruction[0] == "on";

                var newRegion = new Region(int.Parse(instruction[2]), int.Parse(instruction[3]), int.Parse(instruction[5]), int.Parse(instruction[6]), int.Parse(instruction[8]), int.Parse(instruction[9]));

                if ((newRegion.MaxX < -50 || newRegion.MinX > 50) || (newRegion.MaxY < -50 || newRegion.MinY > 50) || (newRegion.MaxZ < -50 || newRegion.MinZ > 50))
                {
                    continue;
                }

                reactor.SwitchRegion(newRegion, on);
            }

            return reactor.ActiveCubeCount().ToString();
        }
    }
}
