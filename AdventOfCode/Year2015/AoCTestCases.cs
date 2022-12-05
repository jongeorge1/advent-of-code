namespace AdventOfCode.Year2015
{
    using AdventOfCode;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "(())", "0")]
        [TestCase(1, 1, "()()", "0")]
        [TestCase(1, 1, "(((", "3")]
        [TestCase(1, 1, "(()(()(", "3")]
        [TestCase(1, 1, "))(((((", "3")]
        [TestCase(1, 1, "())", "-1")]
        [TestCase(1, 1, "))(", "-1")]
        [TestCase(1, 1, ")))", "-3")]
        [TestCase(1, 1, ")())())", "-3")]
        [TestCase(1, 2, ")", "1")]
        [TestCase(1, 2, "()())", "5")]
        [TestCase(2, 1, "2x3x4", "58")]
        [TestCase(2, 1, "1x1x10", "43")]
        [TestCase(2, 1, "2x3x4\r\n1x1x10", "101")]
        [TestCase(2, 2, "2x3x4", "34")]
        [TestCase(2, 2, "1x1x10", "14")]
        [TestCase(2, 2, "2x3x4\r\n1x1x10", "48")]
        [TestCase(3, 1, ">", "2")]
        [TestCase(3, 1, "^>v<", "4")]
        [TestCase(3, 1, "^v^v^v^v^v", "2")]
        [TestCase(3, 2, "^v", "3")]
        [TestCase(3, 2, "^>v<", "3")]
        [TestCase(3, 2, "^v^v^v^v^v", "11")]
        [TestCase(4, 1, "abcdef", "609043")]
        [TestCase(4, 1, "pqrstuv", "1048970")]
        [TestCase(5, 1, "ugknbfddgicrmopn", "1")]
        [TestCase(5, 1, "aaa", "1")]
        [TestCase(5, 1, "jchzalrnumimnmhp", "0")]
        [TestCase(5, 1, "haegwjzuvuyypxyu", "0")]
        [TestCase(5, 1, "dvszwmarrgswjxmb", "0")]
        [TestCase(5, 1, "ugknbfddgicrmopn\r\naaa\r\njchzalrnumimnmhp\r\nhaegwjzuvuyypxyu\r\ndvszwmarrgswjxmb", "2")]
        [TestCase(5, 2, "qjhvhtzxzqqjkmpb", "1")]
        [TestCase(5, 2, "xxyxx", "1")]
        [TestCase(5, 2, "uurcxstgmygtbstg", "0")]
        [TestCase(5, 2, "ieodomkazucvgmuy", "0")]
        [TestCase(6, 1, "turn on 0,0 through 999,999", "1000000")]
        [TestCase(6, 1, "toggle 0,0 through 999,0", "1000")]
        [TestCase(6, 1, "turn on 499,499 through 500,500", "4")]
        [TestCase(6, 2, "turn on 0,0 through 0,0", "1")]
        [TestCase(6, 2, "toggle 0,0 through 999,999", "2000000")]
        [TestCase(7, 1, "123 -> a\r\n456 -> y\r\na AND y -> d\r\na OR y -> e\r\na LSHIFT 2 -> f\r\ny RSHIFT 2 -> g\r\nNOT a -> h\r\nNOT y -> i", "123")]
        [TestCase(9, 1, "London to Dublin = 464\r\nLondon to Belfast = 518\r\nDublin to Belfast = 141", "605")]
        [TestCase(12, 1, "[1,2,3]", "6")]
        [TestCase(12, 1, "{\"a\":2,\"b\":4}", "6")]
        [TestCase(12, 1, "[[[3]]]", "3")]
        [TestCase(12, 1, "{\"a\":{\"b\":4},\"c\":-1}", "3")]
        [TestCase(12, 1, "{\"a\":[-1,1]}", "0")]
        [TestCase(12, 1, "[-1,{\"a\":1}] ", "0")]
        [TestCase(12, 1, "[]", "0")]
        [TestCase(12, 1, "{}", "0")]
        [TestCase(12, 2, "[1,2,3]", "6")]
        [TestCase(12, 2, "[1,{\"c\":\"red\",\"b\":2},3]", "4")]
        [TestCase(12, 2, "{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", "0")]
        [TestCase(12, 2, "[1,\"red\",5]", "6")]
        [TestCase(13, 1, "Alice would gain 54 happiness units by sitting next to Bob.\r\nAlice would lose 79 happiness units by sitting next to Carol.\r\nAlice would lose 2 happiness units by sitting next to David.\r\nBob would gain 83 happiness units by sitting next to Alice.\r\nBob would lose 7 happiness units by sitting next to Carol.\r\nBob would lose 63 happiness units by sitting next to David.\r\nCarol would lose 62 happiness units by sitting next to Alice.\r\nCarol would gain 60 happiness units by sitting next to Bob.\r\nCarol would gain 55 happiness units by sitting next to David.\r\nDavid would gain 46 happiness units by sitting next to Alice.\r\nDavid would lose 7 happiness units by sitting next to Bob.\r\nDavid would gain 41 happiness units by sitting next to Carol.", "330")]
        ////[TestCase(15, 1, "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8\r\nCinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3", "62842880")]
        [TestCase(17, 1, "20\r\n15\r\n10\r\n5\r\n5", "4")]
        [TestCase(18, 1, ".#.#.#\r\n...##.\r\n#....#\r\n..#...\r\n#.#..#\r\n####..", "4")]
        [TestCase(18, 2, ".#.#.#\r\n...##.\r\n#....#\r\n..#...\r\n#.#..#\r\n####..", "14")] // Note the sample shows 5 steps and 17 lights
        [TestCase(19, 1, "H => HO\r\nH => OH\r\nO => HH\r\n\r\nHOH", "4")]
        [TestCase(19, 2, "e => H\r\ne => O\r\nH => HO\r\nH => OH\r\nO => HH\r\n\r\nHOH", "3")]
        [TestCase(19, 2, "e => H\r\ne => O\r\nH => HO\r\nH => OH\r\nO => HH\r\n\r\nHOHOHO", "6")]
        [TestCase(22, 1, "test1", "226")]
        [TestCase(22, 1, "test2", "641")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(2015, day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}