﻿namespace AdventOfCode.Year2019.Day05
{
    using System.Linq;
    using AdventOfCode.Year2019.IntCodeVm;

    public class Part02 : ISolution
    {
        public string Solve(string[] data)
        {
            var vm = new AsyncIntCodeVm(data[0]);
            long[] outputs = vm.Execute(5);

            return outputs.LastOrDefault().ToString();
        }
    }
}
