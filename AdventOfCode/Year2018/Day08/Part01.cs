namespace AdventOfCode.Year2018.Day08
{
    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            var licenceFile = LicenceFile.Parse(input[0]);

            return licenceFile.RootNodes.MetadataChecksum().ToString();
        }
    }
}
