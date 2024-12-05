namespace AdventOfCode.Year2021.Day18;

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SnailfishNumberTests
{
    [TestMethod]
    [DataRow(1, 2)]
    [DataRow(11, 9)]
    [DataRow(4, 14)]
    [DataRow(15, 14)]
    [DataRow(105, 114)]
    public void ParsingAPairWithTwoLiteralValues(int left, int right)
    {
        ReadOnlySpan<char> input = $"[{left},{right}]".AsSpan();
        var result = SnailfishNumber.Parse(ref input);

        Assert.IsInstanceOfType<SnailfishNumberPair>(result);

        var pair = result as SnailfishNumberPair;

        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair!.Left);
        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair!.Right);

        Assert.AreEqual(left, pair.Left.As<SnailfishNumberLiteral>().Value);
        Assert.AreEqual(right, pair.Right.As<SnailfishNumberLiteral>().Value);
    }

    [TestMethod]
    public void ParsingAPairWithANestedPairAndALiteral()
    {
        ReadOnlySpan<char> input = "[[1,4],6]".AsSpan();

        var result = SnailfishNumber.Parse(ref input);

        Assert.IsInstanceOfType<SnailfishNumberPair>(result);

        var pair = result as SnailfishNumberPair;

        Assert.IsInstanceOfType<SnailfishNumberPair>(pair!.Left);
        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair!.Right);

        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair.LeftAs<SnailfishNumberPair>().Left);
        Assert.AreEqual(1, pair.LeftAs<SnailfishNumberPair>().LeftAs<SnailfishNumberLiteral>().Value);

        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair.LeftAs<SnailfishNumberPair>().Right);
        Assert.AreEqual(4, pair.LeftAs<SnailfishNumberPair>().RightAs<SnailfishNumberLiteral>().Value);

        Assert.AreEqual(6, pair.RightAs<SnailfishNumberLiteral>().Value);
    }

    [TestMethod]
    public void ParsingAPairWithALiteralAndANestedPair()
    {
        ReadOnlySpan<char> input = "[6,[1,4]]".AsSpan();

        var result = SnailfishNumber.Parse(ref input);

        Assert.IsInstanceOfType<SnailfishNumberPair>(result);

        var pair = result as SnailfishNumberPair;

        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair!.Left);
        Assert.IsInstanceOfType<SnailfishNumberPair>(pair!.Right);

        Assert.AreEqual(6, pair.LeftAs<SnailfishNumberLiteral>().Value);

        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair.RightAs<SnailfishNumberPair>().Left);
        Assert.AreEqual(1, pair.RightAs<SnailfishNumberPair>().LeftAs<SnailfishNumberLiteral>().Value);

        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair.RightAs<SnailfishNumberPair>().Right);
        Assert.AreEqual(4, pair.RightAs<SnailfishNumberPair>().RightAs<SnailfishNumberLiteral>().Value);
    }

    [TestMethod]
    public void ParsingAPairWithTwoNestedPairs()
    {
        ReadOnlySpan<char> input = "[[5,10],[1,4]]".AsSpan();

        var result = SnailfishNumber.Parse(ref input);

        Assert.IsInstanceOfType<SnailfishNumberPair>(result);

        var pair = result as SnailfishNumberPair;

        Assert.IsInstanceOfType<SnailfishNumberPair>(pair!.Left);
        Assert.IsInstanceOfType<SnailfishNumberPair>(pair!.Right);

        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair.LeftAs<SnailfishNumberPair>().Left);
        Assert.AreEqual(5, pair.LeftAs<SnailfishNumberPair>().LeftAs<SnailfishNumberLiteral>().Value);

        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair.LeftAs<SnailfishNumberPair>().Right);
        Assert.AreEqual(10, pair.LeftAs<SnailfishNumberPair>().RightAs<SnailfishNumberLiteral>().Value);

        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair.RightAs<SnailfishNumberPair>().Left);
        Assert.AreEqual(1, pair.RightAs<SnailfishNumberPair>().LeftAs<SnailfishNumberLiteral>().Value);

        Assert.IsInstanceOfType<SnailfishNumberLiteral>(pair.RightAs<SnailfishNumberPair>().Right);
        Assert.AreEqual(4, pair.RightAs<SnailfishNumberPair>().RightAs<SnailfishNumberLiteral>().Value);
    }

    [TestMethod]
    public void ParsingAPairWithMultipleLevelsOfNestedPairs()
    {
        ReadOnlySpan<char> input = "[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]".AsSpan();

        var result = SnailfishNumber.Parse(ref input);

        Assert.IsInstanceOfType<SnailfishNumberPair>(result);
    }

    [TestMethod]
    public void SimpleAdditionThatDoesntRequireReduction()
    {
        var left = SnailfishNumber.Parse("[1,2]");
        var right = SnailfishNumber.Parse("[[3,4],5]");

        SnailfishNumber sum = left + right;

        Assert.AreEqual("[[1,2],[[3,4],5]]", sum.ToString());
    }

    [TestMethod]
    [DataRow("[1,1]\r\n[2,2]\r\n[3,3]\r\n[4,4]", "[[[[1,1],[2,2]],[3,3]],[4,4]]")]
    [DataRow("[1,1]\r\n[2,2]\r\n[3,3]\r\n[4,4]\r\n[5,5]", "[[[[3,0],[5,3]],[4,4]],[5,5]]")]
    [DataRow("[1,1]\r\n[2,2]\r\n[3,3]\r\n[4,4]\r\n[5,5]\r\n[6,6]", "[[[[5,0],[7,4]],[5,5]],[6,6]]")]
    [DataRow("[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]\r\n[[[5,[2, 8]], 4],[5,[[9,9],0]]]\r\n[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]\r\n[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]\r\n[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]\r\n[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]\r\n[[[[5,4],[7,7]],8],[[8,3],8]]\r\n[[9,3],[[9,9],[6,[4,9]]]]\r\n[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]\r\n[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]", "[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]")]
    public void AdditionThatDoesRequireReduction(string input, string expected)
    {
        SnailfishNumber[] numbers = input.Split(Environment.NewLine).Select(SnailfishNumber.Parse).ToArray();

        SnailfishNumber sum = numbers[0];
        foreach (SnailfishNumber number in numbers[1..])
        {
            sum += number;
        }

        Assert.AreEqual(expected, sum.ToString());
    }

    [TestMethod]
    [DataRow("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
    [DataRow("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
    [DataRow("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
    [DataRow("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
    [DataRow("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
    public void Explosion(string input, string expected)
    {
        var number = SnailfishNumber.Parse(input);
        bool exploded = number.ExplodeIfPossible();

        Assert.IsTrue(exploded);
        Assert.AreEqual(expected, number.ToString());
    }

    [TestMethod]
    [DataRow("[[[[0,7],4],[15,[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,13]]],[1,1]]")]
    [DataRow("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]")]
    public void Split(string input, string expected)
    {
        var number = SnailfishNumber.Parse(input);
        bool split = number.SplitIfPossible();

        Assert.IsTrue(split);
        Assert.AreEqual(expected, number.ToString());
    }

    [TestMethod]
    [DataRow("[[1,2],[[3,4],5]]", 143)]
    [DataRow("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
    [DataRow("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
    [DataRow("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
    [DataRow("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
    [DataRow("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
    public void Magnitude(string input, int expected)
    {
        var number = SnailfishNumber.Parse(input);
        int magnitude = number.Magnitude();

        Assert.AreEqual(expected, magnitude);
    }
}
