namespace AdventOfCode.Helpers;

using System;
using System.Linq;

public static class InputHelpers
{
    public static int[] ParseIntArray(string input, char separator = ',', StringSplitOptions options = StringSplitOptions.None)
    {
        return [.. input.Split(separator, options).Select(int.Parse)];
    }

    public static long[] ParseLongArray(string input, char separator = ',', StringSplitOptions options = StringSplitOptions.None)
    {
        return [.. input.Split(separator, options).Select(long.Parse)];
    }
}
