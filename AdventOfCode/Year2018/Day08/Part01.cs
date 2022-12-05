namespace AdventOfCode.Year2018.Day08
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var licenceFile = LicenceFile.Parse(input);

            return licenceFile.RootNodes.MetadataChecksum().ToString();
        }
    }
}
