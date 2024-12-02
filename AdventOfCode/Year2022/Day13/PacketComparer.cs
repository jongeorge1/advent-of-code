namespace AdventOfCode.Year2022.Day13
{
    using System;
    using System.Diagnostics;

    public static class PacketComparer
    {
        public static PacketTokenComparisonResult ComparePackets(ReadOnlySpan<char> left, ReadOnlySpan<char> right)
        {
            var leftPacketEnumerator = new PacketTokenEnumerator(left);
            var rightPacketEnumerator = new PacketTokenEnumerator(right);

            // Start the enumeration. Once we've done this, we should be on a "list start" for both enumerators
            leftPacketEnumerator.MoveNext();
            rightPacketEnumerator.MoveNext();

            return CompareLists(ref leftPacketEnumerator, ref rightPacketEnumerator);
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
            Debug.Assert(left.Current.TokenType == PacketTokenType.ListStart, "Unexpected situation");
            Debug.Assert(right.Current.TokenType == PacketTokenType.Number, "Unexpected situation");

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

            PacketTokenComparisonResult comparisonResult = CompareNumbers(ref left, ref right);

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

            PacketTokenComparisonResult comparisonResult = CompareNumbers(ref left, ref right);

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
                PacketTokenComparisonResult comparisonResult = CompareAtCurrentToken(ref left, ref right);
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
    }
}
