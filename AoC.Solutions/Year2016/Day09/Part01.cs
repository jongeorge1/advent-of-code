namespace AoC.Solutions.Year2016.Day09
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            int start = 0;
            int pos = input.IndexOf('(');
            int output = 0;

            while (pos != -1 && pos < input.Length)
            {
                output += pos - start;

                // Find the end index, pull out the numbers
                int end = input.IndexOf(')', pos);
                string commandStr = input[(pos + 1) .. end];
                string[] commandSections = commandStr.Split('x');
                int repeats = int.Parse(commandSections[1]);
                int len = int.Parse(commandSections[0]);

                string target = input.Substring(end + 1, len);

                output += target.Length * repeats;

                start = end + len + 1;
                pos = input.IndexOf('(', start);
            }

            if (start < input.Length)
            {
                output += input.Length - start;
            }

            return output.ToString();
        }
    }
}
