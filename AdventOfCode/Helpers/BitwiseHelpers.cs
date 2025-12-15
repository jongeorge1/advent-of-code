namespace AdventOfCode.Helpers;

using System;

public static class BitwiseHelpers
{
    public static long EncodeAsInt64(int[] components, int bitsPerNumber)
    {
        int maxNumbers = 64 / bitsPerNumber;

        if (components.Length > maxNumbers)
        {
            throw new ArgumentException("Array too long to encode as Int64.");
        }

        int mask = (int)((1L << bitsPerNumber) - 1);

        long result = 0;

        for (int i = 0; i < components.Length; i++)
        {
            int maskedComponent = components[i] &= mask;
            if (maskedComponent != components[i])
            {
                throw new ArgumentException($"Component at index {i} has value {components[i]} which exceeds the maximum value of {mask} for {bitsPerNumber} bits.");
            }

            result |= (long)components[i] << (bitsPerNumber * i);
        }

        return result;
    }

    public static int[] DecodeFromInt64(long value, int componentCount, int bitsPerNumber)
    {
        int maxNumbers = 64 / bitsPerNumber;

        if (componentCount < 1 || componentCount > maxNumbers)
        {
            throw new ArgumentException($"Component count must be between 1 and {maxNumbers}.");
        }

        int mask = (int)((1L << bitsPerNumber) - 1);
        int[] components = new int[componentCount];

        for (int i = componentCount - 1; i >= 0; i--)
        {
            components[i] = (int)((value >> (bitsPerNumber * i)) & mask);
        }

        return components;
    }
}
