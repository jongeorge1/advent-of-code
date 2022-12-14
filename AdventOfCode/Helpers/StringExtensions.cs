﻿namespace AdventOfCode.Helpers
{
    using System;

    public static class StringExtensions
    {
        public static LineSplitEnumerator SplitLines(this string str)
        {
            // LineSplitEnumerator is a struct so there is no allocation here
            return new LineSplitEnumerator(str.AsSpan());
        }

        // Must be a ref struct as it contains a ReadOnlySpan<char>
        public ref struct LineSplitEnumerator
        {
            private ReadOnlySpan<char> str;

            public LineSplitEnumerator(ReadOnlySpan<char> str)
            {
                this.str = str;
                this.Current = default;
            }

            public LineSplitEntry Current { get; private set; }

            // Needed to be compatible with the foreach operator
            public LineSplitEnumerator GetEnumerator() => this;

            public bool MoveNext()
            {
                ReadOnlySpan<char> span = this.str;

                // Reach the end of the string
                if (span.Length == 0)
                {
                    return false;
                }

                var index = span.IndexOfAny('\r', '\n');
                if (index == -1)
                {
                    // The string is composed of only one line
                    this.str = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
                    this.Current = new LineSplitEntry(span, ReadOnlySpan<char>.Empty);
                    return true;
                }

                if (index < span.Length - 1 && span[index] == '\r')
                {
                    // Try to consume the '\n' associated to the '\r'
                    var next = span[index + 1];
                    if (next == '\n')
                    {
                        this.Current = new LineSplitEntry(span.Slice(0, index), span.Slice(index, 2));
                        this.str = span.Slice(index + 2);
                        return true;
                    }
                }

                this.Current = new LineSplitEntry(span.Slice(0, index), span.Slice(index, 1));
                this.str = span.Slice(index + 1);
                return true;
            }
        }

        public readonly ref struct LineSplitEntry
        {
            public LineSplitEntry(ReadOnlySpan<char> line, ReadOnlySpan<char> separator)
            {
                this.Line = line;
                this.Separator = separator;
            }

            public ReadOnlySpan<char> Line { get; }

            public ReadOnlySpan<char> Separator { get; }

            // This method allow to implicitly cast the type into a ReadOnlySpan<char>, so you can write the following code
            // foreach (ReadOnlySpan<char> entry in str.SplitLines())
            public static implicit operator ReadOnlySpan<char>(LineSplitEntry entry) => entry.Line;

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
