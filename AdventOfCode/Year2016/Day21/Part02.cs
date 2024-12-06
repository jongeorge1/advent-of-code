namespace AdventOfCode.Year2016.Day21
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            string scrambled = "fbgdceah";

            if (input[0] == "TEST")
            {
                scrambled = "decab";
                input = input[1..];
            }

            string[] instructions = input.Reverse().ToArray();

            char[] unscrambled = scrambled.ToCharArray();

            foreach (string instruction in instructions)
            {
                string[] components = instruction.Split(' ');

                if (instruction.StartsWith("swap position"))
                {
                    unscrambled = UnSwapPosition(unscrambled, components);
                }
                else if (instruction.StartsWith("swap letter"))
                {
                    unscrambled = UnSwapLetter(unscrambled, components);
                }
                else if (instruction.StartsWith("rotate left"))
                {
                    unscrambled = UnRotateLeft(unscrambled, components);
                }
                else if (instruction.StartsWith("rotate right"))
                {
                    unscrambled = UnRotateRight(unscrambled, components);
                }
                else if (instruction.StartsWith("rotate based"))
                {
                    unscrambled = UnRotateBased(unscrambled, components);
                }
                else if (instruction.StartsWith("reverse positions"))
                {
                    unscrambled = UnReverse(unscrambled, components);
                }
                else if (instruction.StartsWith("move position"))
                {
                    unscrambled = UnMovePosition(unscrambled, components);
                }
                else
                {
                    throw new Exception("Unrecognised command");
                }
            }

            return new string(unscrambled);
        }

        private static char[] UnSwapPosition(char[] current, string[] commandSegments)
        {
            // This is identical to the original swap
            char[] result = (char[])current.Clone();

            int xIndex = int.Parse(commandSegments[2]);
            int yIndex = int.Parse(commandSegments[5]);

            result[xIndex] = current[yIndex];
            result[yIndex] = current[xIndex];

            return result;
        }

        private static char[] UnSwapLetter(char[] current, string[] commandSegments)
        {
            // This is identical to the original swap
            char[] result = (char[])current.Clone();

            int xIndex = Array.IndexOf(current, commandSegments[2][0]);
            int yIndex = Array.IndexOf(current, commandSegments[5][0]);

            result[xIndex] = current[yIndex];
            result[yIndex] = current[xIndex];

            return result;
        }

        private static char[] UnRotateLeft(char[] current, string[] commandSegments)
        {
            // This is obviously our previous rotate right code
            char[] result = new char[current.Length];

            int steps = int.Parse(commandSegments[2]);
            steps %= current.Length;

            Array.Copy(current, 0, result, steps, current.Length - steps);
            Array.Copy(current, current.Length - steps, result, 0, steps);

            return result;
        }

        private static char[] UnRotateRight(char[] current, string[] commandSegments)
        {
            int steps = int.Parse(commandSegments[2]);
            return UnRotateRight(current, steps);
        }

        private static char[] UnRotateRight(char[] current, int steps)
        {
            // This is obviously our previous rotate left
            char[] result = new char[current.Length];
            steps %= current.Length;

            Array.Copy(current, steps, result, 0, current.Length - steps);
            Array.Copy(current, 0, result, result.Length - steps, steps);

            return result;
        }

        private static char[] UnRotateBased(char[] current, string[] commandSegments)
        {
            // This is the hardest; because we can't tell what the original position of
            // the letter is, we have to check all the options
            char target = commandSegments[6][0];

            for (int originalPosition = current.Length - 1; originalPosition >= 0; --originalPosition)
            {
                int steps = originalPosition > 3 ? originalPosition + 2 : originalPosition + 1;
                char[] candidate = UnRotateRight(current, steps);

                if (candidate[originalPosition] == target)
                {
                    return candidate;
                }
            }

            throw new Exception("Something has gone wrong!");
        }

        private static char[] UnReverse(char[] current, string[] commandSegments)
        {
            // This code is the same as the reverse.
            char[] result = new char[current.Length];
            int x = int.Parse(commandSegments[2]);
            int y = int.Parse(commandSegments[4]);

            char[] reversedSegment = current[x.. (y + 1)].Reverse().ToArray();

            Array.Copy(current, 0, result, 0, x);
            Array.Copy(reversedSegment, 0, result, x, reversedSegment.Length);
            Array.Copy(current, y + 1, result, y + 1, current.Length - y - 1);

            return result;
        }

        private static char[] UnMovePosition(char[] current, string[] commandSegments)
        {
            // Instead of taking source and inserting at destination, we take destination
            // and insert at source.
            int source = int.Parse(commandSegments[2]);
            int destination = int.Parse(commandSegments[5]);

            var result = new List<char>(current);

            char characterToMove = result[destination];
            result.RemoveAt(destination);
            result.Insert(source, characterToMove);

            return result.ToArray();
        }
    }
}