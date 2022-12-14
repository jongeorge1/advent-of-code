namespace AdventOfCode.Year2022.Day13
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using AdventOfCode;
    using AdventOfCode.Helpers;
    using static AdventOfCode.Helpers.StringExtensions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            StringExtensions.LineSplitEnumerator enumerator = input.SplitLines();

            int index = 1;
            int sumOfCorrectlyOrderedPacketIndices = 0;

            do
            {
                enumerator.MoveNext();
                ReadOnlySpan<char> left = enumerator.Current.Line;

                enumerator.MoveNext();
                ReadOnlySpan<char> right = enumerator.Current.Line;

                var leftPacketEnumerator = new PacketTokenEnumerator(left);
                var rightPacketEnumerator = new PacketTokenEnumerator(right);

                // Start the enumeration. Once we've done this, we should be on a "list start" for both enumerators
                leftPacketEnumerator.MoveNext();
                rightPacketEnumerator.MoveNext();

                if (CompareLists(ref leftPacketEnumerator, ref rightPacketEnumerator) == PacketTokenComparisonResult.Correct)
                {
                    sumOfCorrectlyOrderedPacketIndices += index;
                }

                ++index;
            }
            while (enumerator.MoveNext());

            return sumOfCorrectlyOrderedPacketIndices.ToString();
        }

        private static PacketTokenComparisonResult CompareAtCurrentToken(ref PacketTokenEnumerator left, ref PacketTokenEnumerator right)
        {
            if (left.Current.TokenType == PacketTokenType.ListStart && right.Current.TokenType == PacketTokenType.ListStart)
            {
                return CompareLists(ref left, ref right);
            }
            else if (left.Current.TokenType == PacketTokenType.Number && right.Current.TokenType == PacketTokenType.Number)
            {
                return CompareNumbers(ref left, ref right);
            }
            else if (left.Current.TokenType == PacketTokenType.ListStart && right.Current.TokenType == PacketTokenType.Number)
            {
                return CompareListToNumber(ref left, ref right);
            }
            else
            {
                return CompareNumberToList(ref left, ref right);
            }
        }

        private static PacketTokenComparisonResult CompareListToNumber(ref PacketTokenEnumerator left, ref PacketTokenEnumerator right)
        {
            Debug.Assert(left.Current.TokenType == PacketTokenType.ListStart);
            Debug.Assert(right.Current.TokenType == PacketTokenType.Number);

            // Some scenarios:
            // 1. The left list is empty. This is "Correct"
            // 2. The left list has a single number which is less than the right number. This is also correct
            // 3. The left list has a single number which is equal to the right number. This is inconclusive.
            // 4. The left list has a single number which is greater than the right number. This is incorrect.
            // 5. The left list has multiple numbers, with the first being equal to the right. This is incorrect.
            // 6. The left list contains another list
            left.MoveNext();

            if (left.Current.TokenType == PacketTokenType.ListEnd)
            {
                return PacketTokenComparisonResult.Correct;
            }

            if (left.Current.TokenType == PacketTokenType.ListStart)
            {
                return CompareListToNumber(ref left, ref right);
            }

            var comparisonResult = CompareNumbers(ref left, ref right);

            if (comparisonResult == PacketTokenComparisonResult.Indeterminate)
            {
                // Now we have two possibilities; the left hand side's next token is a list end, in which case
                // we're indeterminate, or it's something else, in which case we're incorrect.
                left.MoveNext();
                if (left.Current.TokenType == PacketTokenType.ListEnd)
                {
                    return PacketTokenComparisonResult.Indeterminate;
                }

                return PacketTokenComparisonResult.Incorrect;
            }

            return comparisonResult;
        }


        private static PacketTokenComparisonResult CompareNumberToList(ref PacketTokenEnumerator left, ref PacketTokenEnumerator right)
        {
            Debug.Assert(left.Current.TokenType == PacketTokenType.Number);
            Debug.Assert(right.Current.TokenType == PacketTokenType.ListStart);

            // Some scenarios:
            // 1. The right list is empty. This is "Incorrect"
            // 2. The right list has a single number which is greater than the left number. This is also correct
            // 3. The right list has a single number which is equal to the left number. This is inconclusive.
            // 4. The right list has a single number which is less than the right number. This is incorrect.
            // 5. The right list has multiple numbers, with the first being equal to the right. This is correct.
            // 6. The rightlist contains another list
            right.MoveNext();

            if (right.Current.TokenType == PacketTokenType.ListEnd)
            {
                return PacketTokenComparisonResult.Incorrect;
            }

            if (right.Current.TokenType == PacketTokenType.ListStart)
            {
                return CompareNumberToList(ref left, ref right);
            }

            var comparisonResult = CompareNumbers(ref left, ref right);

            if (comparisonResult == PacketTokenComparisonResult.Indeterminate)
            {
                // Now we have two possibilities; the right hand side's next token is a list end, in which case
                // we're indeterminate, or it's something else, in which case we're correct.
                right.MoveNext();
                if (right.Current.TokenType == PacketTokenType.ListEnd)
                {
                    return PacketTokenComparisonResult.Indeterminate;
                }

                return PacketTokenComparisonResult.Correct;
            }

            return comparisonResult;
        }

        private static PacketTokenComparisonResult CompareLists(ref PacketTokenEnumerator left, ref PacketTokenEnumerator right)
        {
            Debug.Assert(left.Current.TokenType == PacketTokenType.ListStart);
            Debug.Assert(right.Current.TokenType == PacketTokenType.ListStart);

            do
            {
                left.MoveNext();
                right.MoveNext();

                if (left.Current.TokenType == PacketTokenType.ListEnd)
                {
                    // We've run out of items on the left. Have we also run out of items on the right?
                    if (right.Current.TokenType == PacketTokenType.ListEnd)
                    {
                        return PacketTokenComparisonResult.Indeterminate;
                    }

                    return PacketTokenComparisonResult.Correct;
                }
                else if (right.Current.TokenType == PacketTokenType.ListEnd)
                {
                    // We've run out of items on the right.
                    return PacketTokenComparisonResult.Incorrect;
                }

                // We're on tokens that we need to compare to determine the result.
                var comparisonResult = CompareAtCurrentToken(ref left, ref right);
                if (comparisonResult != PacketTokenComparisonResult.Indeterminate)
                {
                    return comparisonResult;
                }
            }
            while (true);
        }

        private static PacketTokenComparisonResult CompareNumbers(ref PacketTokenEnumerator left, ref PacketTokenEnumerator right)
        {
            Debug.Assert(left.Current.TokenType == PacketTokenType.Number);
            Debug.Assert(right.Current.TokenType == PacketTokenType.Number);

            int leftNumber = left.Current.AsNumber();
            int rightNumber = right.Current.AsNumber();

            if (leftNumber < rightNumber)
            {
                return PacketTokenComparisonResult.Correct;
            }

            if (leftNumber > rightNumber)
            {
                return PacketTokenComparisonResult.Incorrect;
            }

            return PacketTokenComparisonResult.Indeterminate;
        }

        private ref struct PacketTokenEnumerator
        {
            private ReadOnlySpan<char> packet;

            public PacketTokenEnumerator(ReadOnlySpan<char> packet)
            {
                this.packet = packet;
                this.Current = default;
            }

            public PacketToken Current { get; private set; }

            public PacketTokenEnumerator GetEnumerator() => this;

            public bool MoveNext()
            {
                if (this.packet.Length == 0)
                {
                    // We're at the end.
                    return false;
                }

                if (this.packet[0] == '[' || this.packet[0] == ']')
                {
                    this.Current = new PacketToken(this.packet[0..1]);

                    // A ']' might be followed by a ',', in which case we need to slice the packet at a different point
                    if (this.packet.Length > 1 && this.packet[1] == ',')
                    {
                        this.packet = this.packet[2..];
                    }
                    else
                    {
                        this.packet = this.packet[1..];
                    }
                }
                else
                {
                    // This *should* mean it's a number. Since this is AoC, we're going to take things as they should
                    // be, so no big load of error handling... the next token will either be another number, in which case
                    // we need to look for a comma, or an end-of-list, in which case we look for a closing bracket.
                    int index = 1;
                    while (this.packet[index] != ',' && this.packet[index] != ']')
                    {
                        index++;
                    }

                    this.Current = new PacketToken(this.packet[0..index]);

                    if (this.packet[index] == ',')
                    {
                        this.packet = this.packet[(index + 1)..];
                    }
                    else
                    {
                        this.packet = this.packet[index..];
                    }
                }

                return true;
            }
        }

        private readonly ref struct PacketToken
        {
            public PacketToken(ReadOnlySpan<char> token)
            {
                this.Token = token;
            }

            public ReadOnlySpan<char> Token { get; }

            public PacketTokenType TokenType
            {
                get
                {
                    return this.Token[0] switch
                    {
                        '[' => PacketTokenType.ListStart,
                        ']' => PacketTokenType.ListEnd,
                        _ => PacketTokenType.Number,
                    };
                }
            }

            public int AsNumber()
            {
                if (this.TokenType != PacketTokenType.Number)
                {
                    throw new InvalidOperationException();
                }

                return int.Parse(this.Token);
            }
        }

        private enum PacketTokenType
        {
            ListStart,

            ListEnd,

            Number,
        }

        private enum PacketTokenComparisonResult
        {
            Correct,

            Incorrect,

            Indeterminate
        }
    }
}
