namespace AdventOfCode.Helpers
{
    using System;

    public static class StringExtensions
    {
        public static StringSplitEnumerator OptimizedSplit(this string str, string delimiter)
        {
            // LineSplitEnumerator is a struct so there is no allocation here
            return new StringSplitEnumerator(str.AsSpan(), delimiter.AsSpan());
        }

        public static StringSplitEnumerator OptimizedSplit(this string str, ReadOnlySpan<char> delimiter)
        {
            // LineSplitEnumerator is a struct so there is no allocation here
            return new StringSplitEnumerator(str.AsSpan(), delimiter);
        }

        public static StringSplitEnumerator SplitLines(this ReadOnlySpan<char> str, ReadOnlySpan<char> delimiter)
        {
            // LineSplitEnumerator is a struct so there is no allocation here
            return new StringSplitEnumerator(str, delimiter);
        }

        // Must be a ref struct as it contains a ReadOnlySpan<char>
        public ref struct StringSplitEnumerator
        {
            private readonly ReadOnlySpan<char> delimiter;
            private ReadOnlySpan<char> str;
            private int cumulativeIndex = 0;

            public StringSplitEnumerator(ReadOnlySpan<char> str, ReadOnlySpan<char> delimiter)
            {
                this.str = str;
                this.delimiter = delimiter;
                this.Current = default;
            }

            public StringSplitEntry Current { get; private set; }

            // Needed to be compatible with the foreach operator
            public StringSplitEnumerator GetEnumerator() => this;

            public bool MoveNext()
            {
                ReadOnlySpan<char> span = this.str;

                // Reach the end of the string
                if (span.Length == 0)
                {
                    return false;
                }

                var index = span.IndexOf(this.delimiter);
                if (index == -1)
                {
                    // The string is composed of only one line
                    this.str = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
                    this.Current = new StringSplitEntry(span, ReadOnlySpan<char>.Empty, this.cumulativeIndex);
                    return true;
                }

                this.Current = new StringSplitEntry(span.Slice(0, index), span.Slice(index, this.delimiter.Length), this.cumulativeIndex);
                this.cumulativeIndex += index + this.delimiter.Length;
                this.str = span.Slice(index + this.delimiter.Length);
                return true;
            }
        }

        public readonly ref struct StringSplitEntry
        {
            public StringSplitEntry(ReadOnlySpan<char> line, ReadOnlySpan<char> separator, int startIndex)
            {
                this.Line = line;
                this.Separator = separator;
                this.StartIndex = startIndex;
            }

            public ReadOnlySpan<char> Line { get; }

            public ReadOnlySpan<char> Separator { get; }

            public int StartIndex { get; }

            // This method allow to implicitly cast the type into a ReadOnlySpan<char>, so you can write the following code
            // foreach (ReadOnlySpan<char> entry in str.SplitLines())
            public static implicit operator ReadOnlySpan<char>(StringSplitEntry entry) => entry.Line;

            // This method allow to deconstruct the type, so you can write any of the following code
            // foreach (var entry in str.SplitLines()) { _ = entry.Line; }
            // foreach (var (line, endOfLine) in str.SplitLines()) { _ = line; }
            // https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/deconstruct?WT.mc_id=DT-MVP-5003978#deconstructing-user-defined-types
            public void Deconstruct(out ReadOnlySpan<char> line, out ReadOnlySpan<char> separator)
            {
                line = this.Line;
                separator = this.Separator;
            }
        }
    }
}
