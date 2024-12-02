namespace AdventOfCode.Year2019.Day09
{
    using System.Linq;
    using AdventOfCode.Year2019.IntCodeVm;

    public class Part02 : ISolution
    {
        public string Solve(string[] data)
        {
            var vm = new AsyncIntCodeVm(data[0]);
            long[] output = vm.Execute(2);

            return output.Last().ToString();
        }
    }
}
