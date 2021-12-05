namespace AoC.Solutions.Year2016.Day01
{
    using System.Linq;
    using AoC.Solutions.Helpers;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            (int x, int y)[] movementMatrices = new[]
            {
                (0, 1),
                (1, 0),
                (0, -1),
                (-1, 0),
            };

            string[] directions = input.Split(", ");
            (int x, int y) location = (0, 0);
            int currentDirection = 0;

            foreach (string direction in directions)
            {
                string turn = direction.Substring(0, 1);
                int distance = int.Parse(direction.Substring(1));

                // Change direction
                currentDirection = ((turn == "L" ? currentDirection - 1 : currentDirection + 1) + 4) % 4;

                location.x += distance * movementMatrices[currentDirection].x;
                location.y += distance * movementMatrices[currentDirection].y;
            }

            return Distance.Manhattan(location.x, location.y).ToString();
        }
    }
}
