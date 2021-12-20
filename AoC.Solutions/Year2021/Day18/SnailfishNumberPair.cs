namespace AoC.Solutions.Year2021.Day18
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()}")]
    public class SnailfishNumberPair : SnailfishNumber
    {
        public SnailfishNumberPair(ref ReadOnlySpan<char> input, SnailfishNumberPair? parent = null)
            : base(parent)
        {
            // The span we've been provided as input will be on the opening bracket.
            input = input[1..];
            this.Left = SnailfishNumber.Parse(ref input, this);
            this.Right = SnailfishNumber.Parse(ref input, this);

            if (input.Length > 0)
            {
                input = input[1..];
            }
        }

        public SnailfishNumberPair(SnailfishNumber left, SnailfishNumber right, SnailfishNumberPair? parent = null)
            : base(parent)
        {
            this.Left = left;
            this.Right = right;

            this.Left.Parent = this;
            this.Right.Parent = this;
        }

        public SnailfishNumber Left { get; set; }

        public SnailfishNumber Right { get; set; }

        public T LeftAs<T>()
            where T : SnailfishNumber => this.Left.As<T>();

        public T RightAs<T>()
            where T : SnailfishNumber => this.Right.As<T>();

        public override string ToString() => $"[{this.Left},{this.Right}]";

        public LiteralSnailfishNumber? FindFirstLiteralNumberToLeft()
        {
            if (this.Parent is null)
            {
                // Unlikely, but...
                return null;
            }

            // We need to move up the tree until we hit an opportunity to move down a left most branch.
            SnailfishNumberPair current = this;

            while (current.Parent.Left == current)
            {
                current = current.Parent;

                if (current.Parent is null)
                {
                    return null;
                }
            }

            // We should now have hit a point where moving down to the left, then following the right hand branches all the way to the
            // bottom gives us what we need.
            SnailfishNumber target = current.Parent.Left;

            while (target is SnailfishNumberPair targetPair)
            {
                target = targetPair.Right;
            }

            // target should now be a literal
            return target.As<LiteralSnailfishNumber>();
        }

        public LiteralSnailfishNumber? FindFirstLiteralNumberToRight()
        {
            if (this.Parent is null)
            {
                // Unlikely, but...
                return null;
            }

            // We need to move up the tree until we hit an opportunity to move down a left most branch.
            SnailfishNumberPair current = this;

            while (current.Parent.Right == current)
            {
                current = current.Parent;

                if (current.Parent is null)
                {
                    return null;
                }
            }

            // We should now have hit a point where moving down to the right, then following the left hand branches all the way to the
            // bottom gives us what we need.
            SnailfishNumber target = current.Parent.Right;

            while (target is SnailfishNumberPair targetPair)
            {
                target = targetPair.Left;
            }

            // target should now be a literal
            return target.As<LiteralSnailfishNumber>();
        }

        public void ReplaceWithValue(int value)
        {
            SnailfishNumberPair? parent = this.Parent;

            if (parent.Left == this)
            {
                parent.Left = new LiteralSnailfishNumber(0, parent);
            }
            else
            {
                parent.Right = new LiteralSnailfishNumber(0, parent);
            }
        }

        public override SnailfishNumberPair? FindFirstNumberPairToExplode()
        {
            if (this.Depth == 4)
            {
                return this;
            }

            return this.Left.FindFirstNumberPairToExplode() ?? this.Right.FindFirstNumberPairToExplode();
        }

        public override LiteralSnailfishNumber? FindFirstLiteralNumberToSplit()
        {
            return this.Left.FindFirstLiteralNumberToSplit() ?? this.Right.FindFirstLiteralNumberToSplit();
        }

        public override int Magnitude()
        {
            return (this.Left.Magnitude() * 3) + (this.Right.Magnitude() * 2);
        }

        public override SnailfishNumber DeepClone()
        {
            return new SnailfishNumberPair(this.Left.DeepClone(), this.Right.DeepClone());
        }
    }
}
