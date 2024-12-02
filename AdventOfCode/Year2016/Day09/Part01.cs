using AdventOfCode.Year2015.Day07;

namespace AdventOfCode.Year2016.Day09
{
    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            string data = input[0];
            int start = 0;
            int pos = data.IndexOf('(');
            int output = 0;

            while (pos != -1 && pos < data.Length)
            {
                output += pos - start;

                // Find the end index, pull out the numbers
                int end = data.IndexOf(')', pos);
                string commandStr = data[(pos + 1) ..end];
                string[] commandSections = commandStr.Split('x');
                int repeats = int.Parse(commandSections[1]);
                int len = int.Parse(commandSections[0]);

                string target = data.Substring(end + 1, len);

                output += target.Length * repeats;

                start = end + len + 1;
                pos = data.IndexOf('(', start);
            }

            if (start < data.Length)
            {
                output += data.Length - start;
            }

            return output.ToString();
        }
    }
}
