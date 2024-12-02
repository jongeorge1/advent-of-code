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
        // Firstly the shapes: We hold them as arrays of bytes, from bottom to top (i.e. the entry with index 0
        // is the bottom.
        // To facilitate the bit shifting we'll need later, our representations will pretend they are at the
        // left of the play space (left being the high bit). To make this easier to read (and change if I'm
        // wrong), then I'll bitshift them here rather than working out the right numbers.
        private static readonly byte[][] Shapes =
        [
            // Line
            [15 << 3],

            // Cross
            [2 << 4, 7 << 4, 2 << 4],

            // Backwards L
            [7 << 4, 1 << 4, 1 << 4],

            // Column
            [1 << 6, 1 << 6, 1 << 6, 1 << 6],

            // Square
            [3 << 5, 3 << 5],
        ];

        private static readonly int[] ShapeMaxLefts = [3, 4, 4, 6, 5];

        public string Solve(string[] input)
        {
            Span<byte> playSpace = stackalloc byte[8800];
            playSpace[0] = 127;

            var seenStates = new Dictionary<PlaySpaceState, (int MaxHeight, int StonesDropped)>();

            short jetPatternIndex = 0;
            short jetPatternLength = (short)input.Length;

            int stonesDropped = 0;
            int currentMaxHeight = 0;

            while (true)
            {
                Debug.Assert(currentMaxHeight >= 0);

                byte currentShape = (byte)(stonesDropped % 5);
                int currentLeft = 2;
                int currentBottom = currentMaxHeight + 4;
                bool landed = false;

                while (!landed)
                {
                    if (input[0][jetPatternIndex] == '<' && CanMoveLeft(currentLeft, currentBottom, ref Shapes[currentShape], ref playSpace))
                    {
                        --currentLeft;
                    }
                    else if (input[0][jetPatternIndex] == '>' && CanMoveRight(currentLeft, currentBottom, ref Shapes[currentShape], ShapeMaxLefts[currentShape], ref playSpace))
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

                PlaySpaceState newState = MemoizeState(currentMaxHeight, currentShape, (short)(jetPatternIndex - 1), ref playSpace);

                // Have we seen this state before?
                if (seenStates.TryGetValue(newState, out (int MaxHeight, int StonesDropped) heightAndCount))
                {
                    // We've hit the repeat.
                    int periodicity = stonesDropped - heightAndCount.StonesDropped;
                    int heightDifference = currentMaxHeight - heightAndCount.MaxHeight;

                    // We're aiming for the height after we've hit 1000000000000 stones dropped. So, we need to remove the number
                    // of stones we've dropped already, then find out how many times we need to repeat this segment to get close to
                    // that target.
                    long repeats = (1000000000000L - stonesDropped) / periodicity;
                    long stonesDroppedDuringTheRepeats = repeats * periodicity;
                    long extraHeightDroppedDuringTheRepeats = heightDifference * repeats;

                    // What's left?
                    int remainingStonesToDrop = (int)((1000000000000L - stonesDropped) % periodicity);

                    // Find the state where we've dropped the correct number of stones
                    KeyValuePair<PlaySpaceState, (int MaxHeight, int StonesDropped)> el = seenStates.Single(x => x.Value.StonesDropped == heightAndCount.StonesDropped + remainingStonesToDrop);

                    long heightDroppedDuringRemainingPeriod = el.Value.MaxHeight - heightAndCount.MaxHeight;

                    long finalMaxHeight = currentMaxHeight + extraHeightDroppedDuringTheRepeats + heightDroppedDuringRemainingPeriod;

                    return finalMaxHeight.ToString();
                }
                else
                {
                    seenStates.Add(newState, (currentMaxHeight, stonesDropped));
                }
            }

            return currentMaxHeight.ToString();
        }

        private static bool CanMoveLeft(int currentLeft, int currentBottom, ref byte[] shape, ref Span<byte> playSpace)
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

        private static bool CanMoveRight(int currentLeft, int currentBottom, ref byte[] shape, int shapeMaxLeft, ref Span<byte> playSpace)
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

        private static bool CanMove(int suggestedLeft, int suggestedBottom, ref byte[] shape, ref Span<byte> playSpace)
        {
            // Now the comparisons.
            for (int shapeRow = 0; shapeRow < shape.Length; ++shapeRow)
            {
                int playSpaceRow = suggestedBottom + shapeRow;
                if ((playSpace[playSpaceRow] & (shape[shapeRow] >> suggestedLeft)) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CanFall(int currentLeft, int currentBottom, ref byte[] shape, ref Span<byte> playSpace)
        {
            if (currentBottom == 1)
            {
                return false;
            }

            return CanMove(currentLeft, currentBottom - 1, ref shape, ref playSpace);
        }

        private static int AddShapeToPlaySpace(int currentLeft, int currentBottom, int currentMaxHeight, ref byte[] shape, ref Span<byte> playSpace)
        {
            for (int shapeRow = 0; shapeRow < shape.Length; ++shapeRow)
            {
                int playSpaceRow = currentBottom + shapeRow;
                playSpace[playSpaceRow] = (byte)(playSpace[playSpaceRow] | (byte)(shape[shapeRow] >> currentLeft));
            }

            return int.Max(currentMaxHeight, currentBottom + shape.Length - 1);
        }

        private PlaySpaceState MemoizeState(int currentMaxHeight, byte currentShapeIndex, short currentJetIndex, ref Span<byte> playSpace)
        {
            // Our memoized state is going to represent the "shape" of the top layer of the play space by recording the
            // distance "down" you need to travel in each column before you hit a rock.
            (short Distance, bool Done)[] shape = new (short, bool)[7];
            int row = currentMaxHeight;

            while (row >= 0 && !(shape[0].Done && shape[1].Done && shape[2].Done && shape[3].Done && shape[4].Done && shape[5].Done && shape[6].Done))
            {
                for (int column = 0; column < 7; ++column)
                {
                    if (!shape[column].Done)
                    {
                        if ((playSpace[row] & (byte)Math.Pow(2, column)) == 0)
                        {
                            ++shape[column].Distance;
                        }
                        else
                        {
                            shape[column].Done = true;
                        }
                    }
                }

                --row;
            }

            return new PlaySpaceState(
                shape[0].Distance,
                shape[1].Distance,
                shape[2].Distance,
                shape[3].Distance,
                shape[4].Distance,
                shape[5].Distance,
                shape[6].Distance,
                currentJetIndex,
                currentShapeIndex);
        }

        private readonly record struct PlaySpaceState(
            short Col0,
            short Col1,
            short Col2,
            short Col3,
            short Col4,
            short Col5,
            short Col6,
            short LastJetPatternIndex,
            byte LastShape)
        {
        }
    }
}
