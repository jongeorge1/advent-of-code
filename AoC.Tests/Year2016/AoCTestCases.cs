namespace AoC.Tests.Year2016
{
    using AoC.Solutions;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "R2, L3", "5")]
        [TestCase(1, 1, "R2, R2, R2", "2")]
        [TestCase(1, 1, "R5, L5, R5, R3", "12")]
        [TestCase(1, 2, "R8, R4, R4, R8", "4")]
        [TestCase(2, 1, "ULL\r\nRRDDD\r\nLURDL\r\nUUUUD", "1985")]
        [TestCase(2, 2, "ULL\r\nRRDDD\r\nLURDL\r\nUUUUD", "5DB3")]
        [TestCase(3, 1, "5 10 25", "0")]
        [TestCase(3, 1, "15 15 25", "1")]
        [TestCase(5, 1, "abc", "18f47a30")]
        [TestCase(5, 2, "abc", "05ace8e3")]
        [TestCase(6, 1, "eedadn\r\ndrvtee\r\neandsr\r\nraavrd\r\natevrs\r\ntsrnev\r\nsdttsa\r\nrasrtv\r\nnssdts\r\nntnada\r\nsvetve\r\ntesnvt\r\nvntsnd\r\nvrdear\r\ndvrsen\r\nenarar", "easter")]
        [TestCase(7, 1, "abba[mnop]qrst", "1")]
        [TestCase(7, 1, "abcd[bddb]xyyx", "0")]
        [TestCase(7, 1, "aaaa[qwer]tyui", "0")]
        [TestCase(7, 1, "ioxxoj[asdfgh]zxcvbn", "1")]
        [TestCase(7, 2, "aba[bab]xyz", "1")]
        [TestCase(7, 2, "xyx[xyx]xyx", "0")]
        [TestCase(7, 2, "aaa[kek]eke", "1")]
        [TestCase(7, 2, "zazbz[bzb]cdb", "1")]
        [TestCase(8, 1, "TEST\r\nrect 3x2\r\nrotate column x=1 by 1\r\nrotate row y=0 by 4\r\nrotate column x=1 by 1", "6")]
        [TestCase(13, 1, "TEST10", "11")]
        [TestCase(14, 1, "abc", "22728")]
        [TestCase(14, 2, "abc", "22551")]
        [TestCase(15, 1, "Disc #1 has 5 positions; at time=0, it is at position 4.\r\nDisc #2 has 2 positions; at time=0, it is at position 1.", "5")]
        [TestCase(16, 1, "TEST10000", "01100")]
        [TestCase(17, 1, "hijkl", "")]
        [TestCase(17, 1, "kglvqrro", "DDUDRLRRUDRD")]
        [TestCase(17, 1, "ulqzkmiv", "DRURDRUDDLLDLUURRDULRLDUUDDDRR")]
        [TestCase(17, 2, "ihgpwlah", "370")]
        [TestCase(17, 2, "kglvqrro", "492")]
        [TestCase(17, 2, "ulqzkmiv", "830")]
        [TestCase(18, 1, "..^^.", "6")]
        [TestCase(18, 1, ".^^.^.^^^^", "38")]
        [TestCase(19, 1, "5", "3")]
        [TestCase(19, 2, "5", "2")]
        [TestCase(20, 1, "5-8\r\n0-2\r\n4-7", "3")]
        [TestCase(21, 1, "TESTswap position 4 with position 0\r\nswap letter d with letter b\r\nreverse positions 0 through 4\r\nrotate left 1 step\r\nmove position 1 to position 4\r\nmove position 3 to position 0\r\nrotate based on position of letter b\r\nrotate based on position of letter d", "decab")]
        [TestCase(21, 2, "TESTswap position 4 with position 0\r\nswap letter d with letter b\r\nreverse positions 0 through 4\r\nrotate left 1 step\r\nmove position 1 to position 4\r\nmove position 3 to position 0\r\nrotate based on position of letter b\r\nrotate based on position of letter d", "abcde")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(2016, day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}