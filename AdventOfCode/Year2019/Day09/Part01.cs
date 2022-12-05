namespace AdventOfCode.Year2019.Day09
{
    using System.Linq;
    using AdventOfCode.Year2019.IntCodeVm;

    public class Part01 : ISolution
    {
        public string Solve(string data)
        {
            var vm = new AsyncIntCodeVm(data);
            long[] output = vm.Execute(1);

            return output.Last().ToString();
        }
    }
}
