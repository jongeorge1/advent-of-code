namespace AoC.Solutions.Year2019.Day05
{
    using System.Linq;
    using AoC.Solutions.Year2019.IntCodeVm;

    public class Part02 : ISolution
    {
        public string Solve(string data)
        {
            var vm = new AsyncIntCodeVm(data);
            long[] outputs = vm.Execute(5);

            return outputs.LastOrDefault().ToString();
        }
    }
}
