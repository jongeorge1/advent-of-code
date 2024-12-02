namespace AdventOfCode.Year2018.Day08
{
    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            var licenceFile = LicenceFile.Parse(input[0]);

            return licenceFile.RootNodes[0].Value().ToString();
        }
    }
}
