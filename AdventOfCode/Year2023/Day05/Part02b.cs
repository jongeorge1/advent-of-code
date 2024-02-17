////namespace AdventOfCode.Year2023.Day05
////{
////    using System.Diagnostics;
////    using System;
////    using AdventOfCode;
////    using AdventOfCode.Helpers;
////    using NUnit.Framework;
////    using System.Collections.Generic;

////    public class Part02 : ISolution
////    {
////        public string Solve(string[] input)
////        {
////            // We can skip over the seeds until later. We need to parse out each of the maps.
////            int currentLine = 2;

////            var maps = new Map[7]
////            {
////                Map.Create(ref input, ref currentLine),
////                Map.Create(ref input, ref currentLine),
////                Map.Create(ref input, ref currentLine),
////                Map.Create(ref input, ref currentLine),
////                Map.Create(ref input, ref currentLine),
////                Map.Create(ref input, ref currentLine),
////                Map.Create(ref input, ref currentLine),
////            };

////            long currentLowestDestination = long.MaxValue;

////            StringExtensions.StringSplitEnumerator seedsEnumerator = input[0].OptimizedSplit(" ");
////            seedsEnumerator.MoveNext();

////            while (seedsEnumerator.MoveNext())
////            {
////                long currentSeedRangeStart = long.Parse(seedsEnumerator.Current.Line);
////                seedsEnumerator.MoveNext();
////                long currentSeedRangeLength = long.Parse(seedsEnumerator.Current.Line);

////                var currentSeedRange = new Range(currentSeedRangeStart, currentSeedRangeStart + currentSeedRangeLength);

////                Range[] currentDestinationRanges = new[] { currentSeedRange };
////                for (int i = 0; i < maps.Length; ++i)
////                {
////                    currentDestinationRanges = maps[i].MapRanges(currentDestinationRanges);
////                }

////                foreach (Range range in currentDestinationRanges)
////                {
////                    if (range.StartInclusive < currentLowestDestination)
////                    {
////                        currentLowestDestination = range.StartInclusive;
////                    }
////                }
////            }

////            return currentLowestDestination.ToString();
////        }

////        private struct Map
////        {
////            public MapItem[] Items { get; init; }

////            public static Map Create(ref string[] input, ref int currentLine)
////            {
////                var items = new MapItem[50];

////                // We're going to assume that the pointer is at the first line in a map, i.e. the one that describes the type
////                // of map, which we absolutely don't care about, so we can skip it.
////                ++currentLine;

////                // Now we iterate over the following lines until we reach the end of the map, which is indicated by a blank line.
////                int currentMapIndex = 0;

////                while (currentLine < input.Length && input[currentLine].Length != 0)
////                {
////                    ReadOnlySpan<char> line = input[currentLine].AsSpan();
////                    int firstSpaceIndex = line.IndexOf(' ');
////                    int secondSpaceIndex = line.LastIndexOf(' ');
////                    items[currentMapIndex++] = new MapItem(
////                        long.Parse(line[..firstSpaceIndex]),
////                        long.Parse(line[(firstSpaceIndex + 1)..secondSpaceIndex]),
////                        long.Parse(line[(secondSpaceIndex + 1)..]));

////                    ++currentLine;
////                }

////                // We're on a blank line, skip forward to the start of the next map
////                ++currentLine;

////                return new Map
////                {
////                    Items = items[..currentMapIndex],
////                };
////            }

////            public Range[] MapRanges(Range[] inputRanges)
////            {
////                List<Range> rangesToProcess = new (inputRanges);
////                List<Range> outputRanges = new ();

////                foreach (MapItem item in this.Items)
////                {
////                    foreach (Range range in rangesToProcess)
////                    {
////                        if (range.StartInclusive >= item.SourceRange.EndExclusive)
////                        {
////                            // This range is entirely after the end of the source range, so we can skip it.
////                            continue;
////                        }

////                        if (range.EndExclusive <= item.SourceRange.StartInclusive)
////                        {
////                            // This range is entirely before the start of the source range, so we can skip it.
////                            continue;
////                        }

////                        // This range overlaps with the source range, so we need to map it.
////                        long newRangeStart = range.StartInclusive + item.MappingOffset;
////                        long newRangeEnd = range.EndExclusive + item.MappingOffset;

////                        if (newRangeStart < item.SourceRange.StartInclusive)
////                        {
////                            newRangeStart = item.SourceRange.StartInclusive;
////                        }

////                        if (newRangeEnd > item.SourceRange.EndExclusive)
////                        {
////                            newRangeEnd = item.SourceRange.EndExclusive;
////                        }

////                        outputRanges.Add(new Range(newRangeStart, newRangeEnd));
////                    }
////                }

////            }
////        }

////        private record Range(long StartInclusive, long EndExclusive)
////        {
////        }

////        [DebuggerDisplay("{SourceRangeStartInclusive} - {SourceRangeEndInclusive}, {MappingOffset}")]
////        private struct MapItem
////        {
////            public MapItem(long destinationRangeStart, long sourceRangeStart, long rangeLength)
////            {
////                this.SourceRange = new Range(sourceRangeStart, sourceRangeStart + rangeLength);
////                this.MappingOffset = destinationRangeStart - sourceRangeStart;
////            }

////            public Range SourceRange { get; }

////            public long MappingOffset { get; }
////        }
////    }
////}
