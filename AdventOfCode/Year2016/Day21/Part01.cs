namespace AdventOfCode.Year2016.Day21
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            string plainText = "abcdefgh";

            if (input[0].StartsWith("TEST"))
            {
                plainText = "abcde";
                input = input[1..];
            }

            char[] scrambled = plainText.ToCharArray();

            foreach (string instruction in input)
            {
                string[] components = instruction.Split(' ');

                if (instruction.StartsWith("swap position"))
                {
                    scrambled = SwapPosition(scrambled, components);
                }
                else if (instruction.StartsWith("swap letter"))
                {
                    scrambled = SwapLetter(scrambled, components);
                }
                else if (instruction.StartsWith("rotate left"))
                {
                    scrambled = RotateLeft(scrambled, components);
                }
                else if (instruction.StartsWith("rotate right"))
                {
                    scrambled = RotateRight(scrambled, components);
                }
                else if (instruction.StartsWith("rotate based"))
                {
                    scrambled = RotateBased(scrambled, components);
                }
                else if (instruction.StartsWith("reverse positions"))
                {
                    scrambled = Reverse(scrambled, components);
                }
                else if (instruction.StartsWith("move position"))
                {
                    scrambled = MovePosition(scrambled, components);
                }
                else
                {
                    throw new Exception("Unrecognised command");
                }
            }

            return new string(scrambled);
        }

        private static char[] SwapPosition(char[] current, string[] commandSegments)
        {
            char[] result = (char[])current.Clone();

            int xIndex = int.Parse(commandSegments[2]);
            int yIndex = int.Parse(commandSegments[5]);

            result[xIndex] = current[yIndex];
            result[yIndex] = current[xIndex];

            return result;
        }

        private static char[] SwapLetter(char[] current, string[] commandSegments)
        {
            char[] result = (char[])current.Clone();

            int xIndex = Array.IndexOf(current, commandSegments[2][0]);
            int yIndex = Array.IndexOf(current, commandSegments[5][0]);

            result[xIndex] = current[yIndex];
            result[yIndex] = current[xIndex];

            return result;
        }

        private static char[] RotateLeft(char[] current, string[] commandSegments)
        {
            char[] result = new char[current.Length];
            int steps = int.Parse(commandSegments[2]) % current.Length;

            Array.Copy(current, steps, result, 0, current.Length - steps);
            Array.Copy(current, 0, result, result.Length - steps, steps);

            return result;
        }

        private static char[] RotateRight(char[] current, string[] commandSegments)
        {
            int steps = int.Parse(commandSegments[2]);
            return RotateRight(current, steps);
        }

        private static char[] RotateRight(char[] current, int steps)
        {
            char[] result = new char[current.Length];
            steps %= current.Length;

            Array.Copy(current, 0, result, steps, current.Length - steps);
            Array.Copy(current, current.Length - steps, result, 0, steps);

            return result;
        }

        private static char[] RotateBased(char[] current, string[] commandSegments)
        {
            char[] result = new char[current.Length];
            int steps = Array.IndexOf(current, commandSegments[6][0]);

            if (steps > 3)
            {
                ++steps;
            }

            ++steps;

            return RotateRight(current, steps);
        }

        private static char[] Reverse(char[] current, string[] commandSegments)
        {
            char[] result = new char[current.Length];
            int x = int.Parse(commandSegments[2]);
            int y = int.Parse(commandSegments[4]);

            char[] reversedSegment = current[x..(y + 1)].Reverse().ToArray();

            Array.Copy(current, 0, result, 0, x);
            Array.Copy(reversedSegment, 0, result, x, reversedSegment.Length);
            Array.Copy(current, y + 1, result, y + 1, current.Length - y - 1);

            return result;
        }

        private static char[] MovePosition(char[] current, string[] commandSegments)
        {
            int source = int.Parse(commandSegments[2]);
            int destination = int.Parse(commandSegments[5]);

            var result = new List<char>(current);

            char characterToMove = result[source];
            result.RemoveAt(source);
            result.Insert(destination, characterToMove);

            return result.ToArray();
        }
    }
}