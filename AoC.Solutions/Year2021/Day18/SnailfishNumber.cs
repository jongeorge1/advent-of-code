namespace AoC.Solutions.Year2021.Day18
{
    using System;

    public abstract class SnailfishNumber
    {
        protected SnailfishNumber(SnailfishNumberPair? parent)
        {
            this.Parent = parent;
        }

        public SnailfishNumberPair? Parent { get; set; }

        public int Depth => this.Parent is null ? 0 : this.Parent.Depth + 1;

        public static SnailfishNumber operator +(SnailfishNumber left, SnailfishNumber right)
        {
            var result = new SnailfishNumberPair(left.DeepCopy(), right.DeepCopy());

            do
            {
            }
            while (result.ExplodeIfPossible() || result.SplitIfPossible());

            return result;
        }

        public static SnailfishNumber Parse(string input)
        {
            ReadOnlySpan<char> inputSpan = input.AsSpan();
            return Parse(ref inputSpan);
        }

        public static SnailfishNumber Parse(ref ReadOnlySpan<char> input, SnailfishNumberPair? parent = null)
        {
            if (input[0].Equals('['))
            {
                // The number we're parsing is a pair.
                return new SnailfishNumberPair(ref input, parent);
            }
            else
            {
                return new SnailfishNumberLiteral(ref input, parent);
            }
        }

        public T As<T>()
            where T : SnailfishNumber
        {
            return (T)this;
        }

        public bool ExplodeIfPossible()
        {
            // We need to search the tree for the first pair that's nested four levels deep
            SnailfishNumberPair? entryToExplode = this.FindFirstNumberPairToExplode();

            // Now to explode the item, we need to find the items to its left and right.
            if (entryToExplode is null)
            {
                return false;
            }

            SnailfishNumberLiteral? leftItem = entryToExplode.FindFirstLiteralNumberToLeft();
            SnailfishNumberLiteral? rightItem = entryToExplode.FindFirstLiteralNumberToRight();

            if (leftItem is not null)
            {
                leftItem.Value += entryToExplode.LeftAs<SnailfishNumberLiteral>().Value;
            }

            if (rightItem is not null)
            {
                rightItem.Value += entryToExplode.RightAs<SnailfishNumberLiteral>().Value;
            }

            entryToExplode.ReplaceWithValue(0);

            return true;
        }

        public bool SplitIfPossible()
        {
            SnailfishNumberLiteral? entryToSplit = this.FindFirstLiteralNumberToSplit();

            if (entryToSplit is null)
            {
                return false;
            }

            entryToSplit.ReplaceWithPair(
                (int)Math.Floor((decimal)entryToSplit.Value / 2),
                (int)Math.Ceiling((decimal)entryToSplit.Value / 2));

            return true;
        }

        public abstract int Magnitude();

        public abstract SnailfishNumberPair? FindFirstNumberPairToExplode();

        public abstract SnailfishNumberLiteral? FindFirstLiteralNumberToSplit();

        public abstract SnailfishNumber DeepCopy();
    }
}
