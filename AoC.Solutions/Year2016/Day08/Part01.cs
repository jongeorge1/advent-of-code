namespace AoC.Solutions.Year2016.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int width = 50;
            int height = 6;

            if (rows[0] == "TEST")
            {
                rows = rows.Skip(1).ToArray();
                width = 7;
                height = 3;
            }

            var display = new Display(height, width);

            foreach (string row in rows)
            {
                // rect 4x1
                // rotate row y=0 by 4
                // rotate column x=0 by 1
                if (row.StartsWith("rect"))
                {
                    string[] components = row.Split(new[] { ' ', 'x' });
                    display.Rect(int.Parse(components[1]), int.Parse(components[2]));
                }
                else if (row.StartsWith("rotate row"))
                {
                    string[] components = row.Split(new[] { ' ', '=' });
                    display.RotateRow(int.Parse(components[3]), int.Parse(components[5]));
                }
                else if (row.StartsWith("rotate column"))
                {
                    string[] components = row.Split(new[] { ' ', '=' });
                    display.RotateColumn(int.Parse(components[3]), int.Parse(components[5]));
                }
            }

            return display.CountLeds().ToString();
        }
    }
}
