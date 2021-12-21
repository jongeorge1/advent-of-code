namespace AoC.Tests.Year2021.Day18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AoC.Solutions.Year2021.Day18;
    using NUnit.Framework;

    public class SnailfishNumberTests
    {
        [TestCase(1, 2)]
        [TestCase(11, 9)]
        [TestCase(4, 14)]
        [TestCase(15, 14)]
        [TestCase(105, 114)]
        public void ParsingAPairWithTwoLiteralValues(int left, int right)
        {
            ReadOnlySpan<char> input = $"[{left},{right}]".AsSpan();
            var result = SnailfishNumber.Parse(ref input);

            Assert.That(result, Is.InstanceOf<SnailfishNumberPair>());

            var pair = result as SnailfishNumberPair;

            Assert.That(pair!.Left, Is.InstanceOf<SnailfishNumberLiteral>());
            Assert.That(pair!.Right, Is.InstanceOf<SnailfishNumberLiteral>());

            Assert.That(pair.Left.As<SnailfishNumberLiteral>().Value, Is.EqualTo(left));
            Assert.That(pair.Right.As<SnailfishNumberLiteral>().Value, Is.EqualTo(right));
        }

        [Test]
        public void ParsingAPairWithANestedPairAndALiteral()
        {
            ReadOnlySpan<char> input = "[[1,4],6]".AsSpan();

            var result = SnailfishNumber.Parse(ref input);

            Assert.That(result, Is.InstanceOf<SnailfishNumberPair>());

            var pair = result as SnailfishNumberPair;

            Assert.That(pair!.Left, Is.InstanceOf<SnailfishNumberPair>());
            Assert.That(pair!.Right, Is.InstanceOf<SnailfishNumberLiteral>());

            Assert.That(pair.LeftAs<SnailfishNumberPair>().Left, Is.InstanceOf<SnailfishNumberLiteral>());
            Assert.That(pair.LeftAs<SnailfishNumberPair>().LeftAs<SnailfishNumberLiteral>().Value, Is.EqualTo(1));

            Assert.That(pair.LeftAs<SnailfishNumberPair>().Right, Is.InstanceOf<SnailfishNumberLiteral>());
            Assert.That(pair.LeftAs<SnailfishNumberPair>().RightAs<SnailfishNumberLiteral>().Value, Is.EqualTo(4));

            Assert.That(pair.RightAs<SnailfishNumberLiteral>().Value, Is.EqualTo(6));
        }

        [Test]
        public void ParsingAPairWithALiteralAndANestedPair()
        {
            ReadOnlySpan<char> input = "[6,[1,4]]".AsSpan();

            var result = SnailfishNumber.Parse(ref input);

            Assert.That(result, Is.InstanceOf<SnailfishNumberPair>());

            var pair = result as SnailfishNumberPair;

            Assert.That(pair!.Left, Is.InstanceOf<SnailfishNumberLiteral>());
            Assert.That(pair!.Right, Is.InstanceOf<SnailfishNumberPair>());

            Assert.That(pair.LeftAs<SnailfishNumberLiteral>().Value, Is.EqualTo(6));

            Assert.That(pair.RightAs<SnailfishNumberPair>().Left, Is.InstanceOf<SnailfishNumberLiteral>());
            Assert.That(pair.RightAs<SnailfishNumberPair>().LeftAs<SnailfishNumberLiteral>().Value, Is.EqualTo(1));

            Assert.That(pair.RightAs<SnailfishNumberPair>().Right, Is.InstanceOf<SnailfishNumberLiteral>());
            Assert.That(pair.RightAs<SnailfishNumberPair>().RightAs<SnailfishNumberLiteral>().Value, Is.EqualTo(4));
        }

        [Test]
        public void ParsingAPairWithTwoNestedPairs()
        {
            ReadOnlySpan<char> input = "[[5,10],[1,4]]".AsSpan();

            var result = SnailfishNumber.Parse(ref input);

            Assert.That(result, Is.InstanceOf<SnailfishNumberPair>());

            var pair = result as SnailfishNumberPair;

            Assert.That(pair!.Left, Is.InstanceOf<SnailfishNumberPair>());
            Assert.That(pair!.Right, Is.InstanceOf<SnailfishNumberPair>());

            Assert.That(pair.LeftAs<SnailfishNumberPair>().Left, Is.InstanceOf<SnailfishNumberLiteral>());
            Assert.That(pair.LeftAs<SnailfishNumberPair>().LeftAs<SnailfishNumberLiteral>().Value, Is.EqualTo(5));

            Assert.That(pair.LeftAs<SnailfishNumberPair>().Right, Is.InstanceOf<SnailfishNumberLiteral>());
            Assert.That(pair.LeftAs<SnailfishNumberPair>().RightAs<SnailfishNumberLiteral>().Value, Is.EqualTo(10));

            Assert.That(pair.RightAs<SnailfishNumberPair>().Left, Is.InstanceOf<SnailfishNumberLiteral>());
            Assert.That(pair.RightAs<SnailfishNumberPair>().LeftAs<SnailfishNumberLiteral>().Value, Is.EqualTo(1));

            Assert.That(pair.RightAs<SnailfishNumberPair>().Right, Is.InstanceOf<SnailfishNumberLiteral>());
            Assert.That(pair.RightAs<SnailfishNumberPair>().RightAs<SnailfishNumberLiteral>().Value, Is.EqualTo(4));
        }

        [Test]
        public void ParsingAPairWithMultipleLevelsOfNestedPairs()
        {
            ReadOnlySpan<char> input = "[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]".AsSpan();

            var result = SnailfishNumber.Parse(ref input);

            Assert.That(result, Is.InstanceOf<SnailfishNumberPair>());
        }

        [Test]
        public void SimpleAdditionThatDoesntRequireReduction()
        {
            var left = SnailfishNumber.Parse("[1,2]");
            var right = SnailfishNumber.Parse("[[3,4],5]");

            SnailfishNumber sum = left + right;

            Assert.That(sum.ToString(), Is.EqualTo("[[1,2],[[3,4],5]]"));
        }

        [TestCase("[1,1]\r\n[2,2]\r\n[3,3]\r\n[4,4]", "[[[[1,1],[2,2]],[3,3]],[4,4]]")]
        [TestCase("[1,1]\r\n[2,2]\r\n[3,3]\r\n[4,4]\r\n[5,5]", "[[[[3,0],[5,3]],[4,4]],[5,5]]")]
        [TestCase("[1,1]\r\n[2,2]\r\n[3,3]\r\n[4,4]\r\n[5,5]\r\n[6,6]", "[[[[5,0],[7,4]],[5,5]],[6,6]]")]
        [TestCase("[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]\r\n[[[5,[2, 8]], 4],[5,[[9,9],0]]]\r\n[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]\r\n[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]\r\n[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]\r\n[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]\r\n[[[[5,4],[7,7]],8],[[8,3],8]]\r\n[[9,3],[[9,9],[6,[4,9]]]]\r\n[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]\r\n[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]", "[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]")]
        public void AdditionThatDoesRequireReduction(string input, string expected)
        {
            SnailfishNumber[] numbers = input.Split(Environment.NewLine).Select(SnailfishNumber.Parse).ToArray();

            SnailfishNumber sum = numbers[0];
            foreach (SnailfishNumber number in numbers[1..])
            {
                sum += number;
            }

            Assert.That(sum.ToString(), Is.EqualTo(expected));
        }

        [TestCase("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
        [TestCase("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
        [TestCase("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
        [TestCase("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
        [TestCase("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
        public void Explosion(string input, string expected)
        {
            var number = SnailfishNumber.Parse(input);
            bool exploded = number.ExplodeIfPossible();

            Assert.That(exploded, Is.True);
            Assert.That(number.ToString(), Is.EqualTo(expected));
        }

        [TestCase("[[[[0,7],4],[15,[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,13]]],[1,1]]")]
        [TestCase("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]")]
        public void Split(string input, string expected)
        {
            var number = SnailfishNumber.Parse(input);
            bool split = number.SplitIfPossible();

            Assert.That(split, Is.True);
            Assert.That(number.ToString(), Is.EqualTo(expected));
        }

        [TestCase("[[1,2],[[3,4],5]]", 143)]
        [TestCase("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
        [TestCase("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
        [TestCase("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
        [TestCase("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
        [TestCase("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
        public void Magnitude(string input, int expected)
        {
            var number = SnailfishNumber.Parse(input);
            int magnitude = number.Magnitude();

            Assert.That(magnitude, Is.EqualTo(expected));
        }
    }
}
