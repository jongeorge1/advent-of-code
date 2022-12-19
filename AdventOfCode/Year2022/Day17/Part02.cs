namespace AdventOfCode.Year2022.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AdventOfCode;
    using NUnit.Framework;

    public class Part02 : ISolution
    {
        // We're going to represent the play space and shapes using bytes.
        // Firstly the shapes: We hold them as arrays of bytes, from bottom to top (i.e. the entry with index 0
        // is the bottom.
        // To facilitate the bit shifting we'll need later, our representations will pretend they are at the
        // left of the play space (left being the high bit). To make this easier to read (and change if I'm 
        // wrong, then I'll bitshift them here rather than working out the right numbers.
        private static readonly byte[][] Shapes = new byte[][]
        {
            // Line
            new byte[] { 15 << 3 },

            // Cross
            new byte[] { 2 << 4, 7 << 4, 2 << 4 },

            // Backwards L
            new byte[] { 7 << 4, 1 << 4, 1 << 4 },

            // Column
            new byte[] { 1 << 6, 1 << 6, 1 << 6, 1 << 6 },

            // Square
            new byte[] { 3 << 5, 3 << 5 },
        };

        private static readonly int[] ShapeMaxLefts = new[] { 3, 4, 4, 6, 5 };

        public string Solve(string input)
        {
            var playSpace = new List<byte>(10000)
            {
                127,
            };

            int jetPatternIndex = 0;
            int jetPatternLength = input.Length;

            int stonesDropped = 0;
            int currentMaxHeight = 0;

            while (stonesDropped < 2022)
            {
                int currentShape = stonesDropped % 5;
                int currentLeft = 2;
                int currentBottom = currentMaxHeight + 4;
                bool landed = false;

                while (!landed)
                {
                    if (input[jetPatternIndex] == '<' && CanMoveLeft(currentLeft, currentBottom, ref Shapes[currentShape], ref playSpace))
                    {
                        --currentLeft;
                    }
                    else if (input[jetPatternIndex] == '>' && CanMoveRight(currentLeft, currentBottom, ref Shapes[currentShape], ShapeMaxLefts[currentShape], ref playSpace))
                    {
                        ++currentLeft;
                    }

                    if (CanFall(currentLeft, currentBottom, ref Shapes[currentShape], ref playSpace))
                    {
                        --currentBottom;
                    }
                    else
                    {
                        currentMaxHeight = AddShapeToPlaySpace(currentLeft, currentBottom, currentMaxHeight, ref Shapes[currentShape], ref playSpace);
                        landed = true;
                    }

                    ++jetPatternIndex;
                    if (jetPatternIndex >= jetPatternLength)
                    {
                        jetPatternIndex = 0;
                    }
                }

                ++stonesDropped;
            }

            return currentMaxHeight.ToString();
        }

        private static bool CanMoveLeft(int currentLeft, int currentBottom, ref byte[] shape, ref List<byte> playSpace)
        {
            // We can move left if:
            // 1. We wouldn't move off the edge. This requires currentLeft > 0, which we can test for immediately.
            // 2. It won't hit anything else. This requires comparison to rows in the playspace
            if (currentLeft == 0)
            {
                return false;
            }

            return CanMove(currentLeft - 1, currentBottom, ref shape, ref playSpace);
        }

        private static bool CanMoveRight(int currentLeft, int currentBottom, ref byte[] shape, int shapeMaxLeft, ref List<byte> playSpace)
        {
            // We can move right if:
            // 1. We wouldn't move off the edge. This requires currentRight < max right for the shape, which we can test for immediately.
            // 2. It won't hit anything else. This requires comparison to rows in the playspace
            if (currentLeft == shapeMaxLeft)
            {
                return false;
            }

            return CanMove(currentLeft + 1, currentBottom, ref shape, ref playSpace);
        }

        private static bool CanMove(int suggestedLeft, int suggestedBottom, ref byte[] shape, ref List<byte> playSpace)
        {
            // Now the comparisons.
            for (int shapeRow = 0; shapeRow < shape.Length; ++shapeRow)
            {
                int playSpaceRow = suggestedBottom + shapeRow;

                if (playSpace.Count <= playSpaceRow)
                {
                    return true;
                }

                if ((playSpace[playSpaceRow] & (shape[shapeRow] >> suggestedLeft)) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CanFall(int currentLeft, int currentBottom, ref byte[] shape, ref List<byte> playSpace)
        {
            if (currentBottom == 1)
            {
                return false;
            }

            return CanMove(currentLeft, currentBottom - 1, ref shape, ref playSpace);
        }

        private static int AddShapeToPlaySpace(int currentLeft, int currentBottom, int currentMaxHeight, ref byte[] shape, ref List<byte> playSpace)
        {
            for (int shapeRow = 0; shapeRow < shape.Length; ++shapeRow)
            {
                int playSpaceRow = currentBottom + shapeRow;
                if (playSpace.Count <= playSpaceRow)
                {
                    playSpace.Add((byte)(shape[shapeRow] >> currentLeft));
                }
                else
                {
                    playSpace[playSpaceRow] = (byte)(playSpace[playSpaceRow] | (byte)(shape[shapeRow] >> currentLeft));
                }
            }

            return int.Max(currentMaxHeight, currentBottom + shape.Length - 1);
        }
    }
}
