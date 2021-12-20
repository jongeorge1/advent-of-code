namespace AoC.Solutions.Year2021.Day17
{
    using System;
    using System.Collections.Generic;
    using AoC.Solutions;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            // target area: x=277..318, y=-92..-53
            string[] components = input.Split(new[] { 'x', 'y', '=', '.', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int minX = int.Parse(components[2]);
            int maxX = int.Parse(components[3]);
            int minY = int.Parse(components[4]);
            int maxY = int.Parse(components[5]);

            // Step 1: Look for x values that could potentially land the probe in our target zone.
            List<(int launchXVelocity, int stepsToXTargetArea)> potentialXValues = new ();

            for (int potentialX = 1; potentialX <= maxX; ++potentialX)
            {
                // For this potential launch velocity, see if there's a number of steps that would land it in the some horizontal
                // area as the target.
                int currentXVelocity = potentialX;
                int currentX = 0;
                int step = 0;
                while (currentXVelocity >= 0)
                {
                    currentX += currentXVelocity;
                    currentXVelocity -= 1;
                    ++step;

                    if (currentX >= minX && currentX <= maxX)
                    {
                        // We're in the zone.
                        potentialXValues.Add((potentialX, step));
                        break;
                    }
                }
            }

            int matchingPathCount = 0;

            // Now for each of those x values, we need to find y values that work
            // Going to make a big assumption that the y we need is positive.
            foreach ((int launchXVelocity, int stepsToXTargetArea) in potentialXValues)
            {
                for (int potentialY = minY; potentialY < 100; ++potentialY)
                {
                    int currentXVelocity = launchXVelocity;
                    int currentYVelocity = potentialY;

                    int currentX = 0;
                    int currentY = 0;

                    while (currentX <= maxX && currentY >= minY)
                    {
                        currentX += currentXVelocity;
                        currentY += currentYVelocity;

                        currentXVelocity = Math.Max(currentXVelocity - 1, 0);
                        --currentYVelocity;

                        if (currentX >= minX && currentX <= maxX && currentY >= minY && currentY <= maxY)
                        {
                            ++matchingPathCount;
                            break;
                        }
                    }
                }
            }

            return matchingPathCount.ToString();
        }
    }
}
