namespace AdventOfCode.Year2015.Day25
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            int x = 1;
            int y = 1;

            long currentValue = 20151125;

            do
            {
                if (y == 1)
                {
                    y = x + 1;
                    x = 1;
                }
                else
                {
                    x += 1;
                    y -= 1;
                }

                currentValue = (currentValue * 252533) % 33554393;
            }
            while (!(x == 3075 && y == 2981));

            return currentValue.ToString();
        }
    }
}