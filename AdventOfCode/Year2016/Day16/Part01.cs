namespace AdventOfCode.Year2016.Day16
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int size = 272;

            if (input[0].StartsWith("TEST"))
            {
                size = 20;
                input[0] = input[0][4..];
            }

            var data = input[0].ToList();

            while (data.Count < size)
            {
                var b = data.Select(x => x == '1' ? '0' : '1').Reverse().ToList();
                data.Add('0');
                data.AddRange(b);
            }

            data = data.Take(size).ToList();

            List<char> checksum = data;

            while (checksum.Count % 2 == 0)
            {
                checksum = Enumerable.Range(0, checksum.Count).Where(x => x % 2 == 0).Select(x => checksum[x] == checksum[x + 1] ? '1' : '0').ToList();
            }

            return new string(checksum.ToArray());
        }
    }
}