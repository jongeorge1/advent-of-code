namespace AdventOfCode.Year2022.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;

    public class MonkeyTests
    {
        [Test]
        public void TestInitialiseWithAdditionOperation()
        {
            var subject = new Monkey("Monkey 1:\r\n  Starting items: 54, 65, 75, 74\r\n  Operation: new = old + 6\r\n  Test: divisible by 19\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 0");

            Assert.AreEqual(1, subject.Number);
            Assert.AreEqual(new[] { 54, 65, 75, 74 }, subject.Items);
            Assert.AreEqual(16, subject.Operation(10));
            Assert.AreEqual(2, subject.ThrowTargets[true]);
            Assert.AreEqual(0, subject.ThrowTargets[false]);
        }

        [Test]
        public void TestInitialiseWithMultiplicationOperation()
        {
            var subject = new Monkey("Monkey 0:\r\n  Starting items: 79, 98\r\n  Operation: new = old * 19\r\n  Test: divisible by 23\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 3");

            Assert.AreEqual(0, subject.Number);
            Assert.AreEqual(new[] { 79, 98 }, subject.Items);
            Assert.AreEqual(38, subject.Operation(2));
            Assert.AreEqual(2, subject.ThrowTargets[true]);
            Assert.AreEqual(3, subject.ThrowTargets[false]);
        }

        [Test]
        public void TestInitialiseWithPowerOperation()
        {
            var subject = new Monkey("Monkey 2:\r\n  Starting items: 79, 60, 97\r\n  Operation: new = old * old\r\n  Test: divisible by 13\r\n    If true: throw to monkey 1\r\n    If false: throw to monkey 3");

            Assert.AreEqual(2, subject.Number);
            Assert.AreEqual(new[] { 79, 60, 97 }, subject.Items);
            Assert.AreEqual(9, subject.Operation(3));
            Assert.AreEqual(1, subject.ThrowTargets[true]);
            Assert.AreEqual(3, subject.ThrowTargets[false]);
        }
    }
}
