namespace AdventOfCode.Year2024.Day11;

using System.Collections.Generic;

public class StoneBreaker
{
    private Dictionary<(long Value, int CurrentDepth), long> cachedValues = new();


    public long BreakStone(long value, int currentDepth, int targetDepth)
    {
        if (this.cachedValues.TryGetValue((value, currentDepth), out long previousResult))
        {
            return previousResult;
        }

        long result = 0;

        if (currentDepth == targetDepth)
        {
            result = 1;
        }
        else if (value == 0)
        {
            result = this.BreakStone(1, currentDepth + 1, targetDepth);

        }
        else
        {
            string currentAsString = value.ToString();
            if (currentAsString.Length % 2 == 0)
            {
                int midPoint = currentAsString.Length / 2;

                result = this.BreakStone(long.Parse(currentAsString[..midPoint]), currentDepth + 1, targetDepth) +
                       this.BreakStone(long.Parse(currentAsString[midPoint..]), currentDepth + 1, targetDepth);
            }
            else
            {
                result = this.BreakStone(value * 2024, currentDepth + 1, targetDepth);
            }
        }

        this.cachedValues[(value, currentDepth)] = result;
        return result;
    }
}
