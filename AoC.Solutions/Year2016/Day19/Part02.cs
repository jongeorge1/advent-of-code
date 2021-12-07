namespace AoC.Solutions.Year2016.Day19
{
    using System;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            int presentCount = int.Parse(input);

            // Find the highest power of three that is less than the number
            int power = 0;
            while (Math.Pow(3, power) < presentCount)
            {
                ++power;
            }

            int previousPowerOfThree = (int)Math.Pow(3, power - 1);

            // Now, we need to find out the difference between that number and the total present count
            int additionalElves = presentCount - previousPowerOfThree;

            if (additionalElves == 0)
            {
                // If the number is exactly a power of three, the last elf always wins.
                return previousPowerOfThree.ToString();
            }

            if (additionalElves <= previousPowerOfThree)
            {
                return additionalElves.ToString();
            }

            return (previousPowerOfThree + ((additionalElves - previousPowerOfThree) * 2)).ToString();
        }
    }
}