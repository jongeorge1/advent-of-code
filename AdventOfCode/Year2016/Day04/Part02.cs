namespace AdventOfCode.Year2016.Day04
{
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            Room[] rooms = input.Select(x => new Room(x)).Where(x => x.IsValid).ToArray();

            Room targetRoom = rooms.First(x => x.DecryptedName().StartsWith("north"));

            return targetRoom.SectorId.ToString();
        }
    }
}
