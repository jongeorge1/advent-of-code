namespace AdventOfCode.Year2016.Day08
{
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            int width = 50;
            int height = 6;

            if (input[0] == "TEST")
            {
                input = input.Skip(1).ToArray();
                width = 7;
                height = 3;
            }

            var display = new Display(height, width);

            foreach (string row in input)
            {
                // rect 4x1
                // rotate row y=0 by 4
                // rotate column x=0 by 1
                if (row.StartsWith("rect"))
                {
                    string[] components = row.Split([' ', 'x']);
                    display.Rect(int.Parse(components[1]), int.Parse(components[2]));
                }
                else if (row.StartsWith("rotate row"))
                {
                    string[] components = row.Split([' ', '=']);
                    display.RotateRow(int.Parse(components[3]), int.Parse(components[5]));
                }
                else if (row.StartsWith("rotate column"))
                {
                    string[] components = row.Split([' ', '=']);
                    display.RotateColumn(int.Parse(components[3]), int.Parse(components[5]));
                }
            }

            string result = display.DrawDisplay();
            return result;
        }
    }
}
