namespace AdventOfCode.Year2023.Day05
{
    using System;
    using System.Diagnostics;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            // We can skip over the seeds until later. We need to parse out each of the maps.
            int currentLine = 2;

            var maps = new Map[7]
            {
                Map.Create(ref input, ref currentLine),
                Map.Create(ref input, ref currentLine),
                Map.Create(ref input, ref currentLine),
                Map.Create(ref input, ref currentLine),
                Map.Create(ref input, ref currentLine),
                Map.Create(ref input, ref currentLine),
                Map.Create(ref input, ref currentLine),
            };

            long currentLowestDestination = long.MaxValue;
            StringExtensions.StringSplitEnumerator seedsEnumerator = input[0].OptimizedSplit(" ");
            seedsEnumerator.MoveNext();

            while (seedsEnumerator.MoveNext())
            {
                long currentSeedRangeStart = long.Parse(seedsEnumerator.Current.Line);
                seedsEnumerator.MoveNext();
                long currentSeedRangeLength = long.Parse(seedsEnumerator.Current.Line);

                for (int seedIndex = 0; seedIndex < currentSeedRangeLength; ++seedIndex)
                {
                    long currentDestination = currentSeedRangeStart + seedIndex;

                    for (int i = 0; i < maps.Length; ++i)
                    {
                        currentDestination = maps[i].MapValue(currentDestination);
                    }

                    if (currentDestination < currentLowestDestination)
                    {
                        currentLowestDestination = currentDestination;
                    }
                }

            }

            return currentLowestDestination.ToString();
        }

        private struct Map
        {
            public MapItem[] Items { get; init; }

            public static Map Create(ref string[] input, ref int currentLine)
            {
                var items = new MapItem[50];

                // We're going to assume that the pointer is at the first line in a map, i.e. the one that describes the type
                // of map, which we absolutely don't care about, so we can skip it.
                ++currentLine;

                // Now we iterate over the following lines until we reach the end of the map, which is indicated by a blank line.
                int currentMapIndex = 0;

                while (currentLine < input.Length && input[currentLine].Length != 0)
                {
                    ReadOnlySpan<char> line = input[currentLine].AsSpan();
                    int firstSpaceIndex = line.IndexOf(' ');
                    int secondSpaceIndex = line.LastIndexOf(' ');
                    items[currentMapIndex++] = new MapItem(
                        long.Parse(line[..firstSpaceIndex]),
                        long.Parse(line[(firstSpaceIndex + 1)..secondSpaceIndex]),
                        long.Parse(line[(secondSpaceIndex + 1)..]));

                    ++currentLine;
                }

                // We're on a blank line, skip forward to the start of the next map
                ++currentLine;

                return new Map
                {
                    Items = items[..currentMapIndex],
                };
            }

            public long MapValue(long value)
            {
                foreach (MapItem item in this.Items)
                {
                    if (value >= item.SourceRangeStartInclusive && value < item.SourceRangeEndExclusive)
                    {
                        return value + item.MappingOffset;
                    }
                }

                return value;
            }
        }

        [DebuggerDisplay("{SourceRangeStartInclusive} - {SourceRangeEndInclusive}, {MappingOffset}")]
        private struct MapItem
        {
            public MapItem(long destinationRangeStart, long sourceRangeStart, long rangeLength)
            {
                this.SourceRangeStartInclusive = sourceRangeStart;
                this.SourceRangeEndExclusive = sourceRangeStart + rangeLength;

                this.MappingOffset = destinationRangeStart - sourceRangeStart;
            }

            public long SourceRangeStartInclusive { get; }

            public long SourceRangeEndExclusive { get; }

            public long MappingOffset { get; }
        }
    }
}
