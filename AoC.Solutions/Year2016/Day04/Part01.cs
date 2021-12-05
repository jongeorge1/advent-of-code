namespace AoC.Solutions.Year2016.Day04
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Room[] rooms = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => new Room(x)).ToArray();
            return rooms.Where(x => x.IsValid).Sum(x => x.SectorId).ToString();
        }
    }
}
