namespace AoC.Tests.Year2021
{
    using AoC.Solutions;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "199\r\n200\r\n208\r\n210\r\n200\r\n207\r\n240\r\n269\r\n260\r\n263", "7")]
        [TestCase(1, 2, "199\r\n200\r\n208\r\n210\r\n200\r\n207\r\n240\r\n269\r\n260\r\n263", "5")]
        [TestCase(2, 1, "forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2", "150")]
        [TestCase(2, 2, "forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2", "900")]
        [TestCase(3, 1, "00100\r\n11110\r\n10110\r\n10111\r\n10101\r\n01111\r\n00111\r\n11100\r\n10000\r\n11001\r\n00010\r\n01010", "198")]
        [TestCase(3, 2, "00100\r\n11110\r\n10110\r\n10111\r\n10101\r\n01111\r\n00111\r\n11100\r\n10000\r\n11001\r\n00010\r\n01010", "230")]
        [TestCase(4, 1, "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1\r\n\r\n22 13 17 11  0\r\n 8  2 23  4 24\r\n21  9 14 16  7\r\n 6 10  3 18  5\r\n 1 12 20 15 19\r\n\r\n 3 15  0  2 22\r\n 9 18 13 17  5\r\n19  8  7 25 23\r\n20 11 10 24  4\r\n14 21 16 12  6\r\n\r\n14 21 17 24  4\r\n10 16 15  9 19\r\n18  8 23 26 20\r\n22 11 13  6  5\r\n 2  0 12  3  7", "4512")]
        [TestCase(4, 2, "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1\r\n\r\n22 13 17 11  0\r\n 8  2 23  4 24\r\n21  9 14 16  7\r\n 6 10  3 18  5\r\n 1 12 20 15 19\r\n\r\n 3 15  0  2 22\r\n 9 18 13 17  5\r\n19  8  7 25 23\r\n20 11 10 24  4\r\n14 21 16 12  6\r\n\r\n14 21 17 24  4\r\n10 16 15  9 19\r\n18  8 23 26 20\r\n22 11 13  6  5\r\n 2  0 12  3  7", "1924")]
        [TestCase(5, 1, "0,9 -> 5,9\r\n8,0 -> 0,8\r\n9,4 -> 3,4\r\n2,2 -> 2,1\r\n7,0 -> 7,4\r\n6,4 -> 2,0\r\n0,9 -> 2,9\r\n3,4 -> 1,4\r\n0,0 -> 8,8\r\n5,5 -> 8,2", "5")]
        [TestCase(5, 2, "0,9 -> 5,9\r\n8,0 -> 0,8\r\n9,4 -> 3,4\r\n2,2 -> 2,1\r\n7,0 -> 7,4\r\n6,4 -> 2,0\r\n0,9 -> 2,9\r\n3,4 -> 1,4\r\n0,0 -> 8,8\r\n5,5 -> 8,2", "12")]
        [TestCase(6, 1, "3,4,3,1,2", "5934")]
        [TestCase(6, 2, "3,4,3,1,2", "26984457539")]
        [TestCase(8, 1, "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe\r\nedbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc\r\nfgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg\r\nfbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb\r\naecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea\r\nfgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb\r\ndbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe\r\nbdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef\r\negadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb\r\ngcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce", "26")]
        [TestCase(8, 2, "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf", "5353")]
        [TestCase(8, 2, "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe\r\nedbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc\r\nfgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg\r\nfbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb\r\naecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea\r\nfgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb\r\ndbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe\r\nbdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef\r\negadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb\r\ngcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce", "61229")]
        [TestCase(9, 1, "2199943210\r\n3987894921\r\n9856789892\r\n8767896789\r\n9899965678", "15")]
        [TestCase(9, 2, "2199943210\r\n3987894921\r\n9856789892\r\n8767896789\r\n9899965678", "1134")]
        [TestCase(10, 1, "{([(<{}[<>[]}>{[]{[(<()>", "1197")]
        [TestCase(10, 1, "[[<[([]))<([[{}[[()]]]", "3")]
        [TestCase(10, 1, "[{[{({}]{}}([{[{{{}}([]", "57")]
        [TestCase(10, 1, "[<(<(<(<{}))><([]([]()", "3")]
        [TestCase(10, 1, "<{([([[(<>()){}]>(<<{{", "25137")]
        [TestCase(10, 1, "[({(<(())[]>[[{[]{<()<>>\r\n[(()[<>])]({[<{<<[]>>(\r\n{([(<{}[<>[]}>{[]{[(<()>\r\n(((({<>}<{<{<>}{[]{[]{}\r\n[[<[([]))<([[{}[[()]]]\r\n[{[{({}]{}}([{[{{{}}([]\r\n{<[[]]>}<{[{[{[]{()[[[]\r\n[<(<(<(<{}))><([]([]()\r\n<{([([[(<>()){}]>(<<{{\r\n<{([{{}}[<[[[<>{}]]]>[]]", "26397")]
        [TestCase(10, 2, "[({(<(())[]>[[{[]{<()<>>", "288957")]
        [TestCase(10, 2, "[(()[<>])]({[<{<<[]>>(", "5566")]
        [TestCase(10, 2, "(((({<>}<{<{<>}{[]{[]{}", "1480781")]
        [TestCase(10, 2, "{<[[]]>}<{[{[{[]{()[[[]", "995444")]
        [TestCase(10, 2, "<{([{{}}[<[[[<>{}]]]>[]]", "294")]
        [TestCase(10, 2, "[({(<(())[]>[[{[]{<()<>>\r\n[(()[<>])]({[<{<<[]>>(\r\n{([(<{}[<>[]}>{[]{[(<()>\r\n(((({<>}<{<{<>}{[]{[]{}\r\n[[<[([]))<([[{}[[()]]]\r\n[{[{({}]{}}([{[{{{}}([]\r\n{<[[]]>}<{[{[{[]{()[[[]\r\n[<(<(<(<{}))><([]([]()\r\n<{([([[(<>()){}]>(<<{{\r\n<{([{{}}[<[[[<>{}]]]>[]]", "288957")]
        [TestCase(11, 1, "5483143223\r\n2745854711\r\n5264556173\r\n6141336146\r\n6357385478\r\n4167524645\r\n2176841721\r\n6882881134\r\n4846848554\r\n5283751526", "1656")]
        [TestCase(11, 2, "5483143223\r\n2745854711\r\n5264556173\r\n6141336146\r\n6357385478\r\n4167524645\r\n2176841721\r\n6882881134\r\n4846848554\r\n5283751526", "195")]
        [TestCase(12, 1, "dc-end\r\nHN-start\r\nstart-kj\r\ndc-start\r\ndc-HN\r\nLN-dc\r\nHN-end\r\nkj-sa\r\nkj-HN\r\nkj-dc", "19")]
        [TestCase(12, 1, "fs-end\r\nhe-DX\r\nfs-he\r\nstart-DX\r\npj-DX\r\nend-zg\r\nzg-sl\r\nzg-pj\r\npj-he\r\nRW-he\r\nfs-DX\r\npj-RW\r\nzg-RW\r\nstart-pj\r\nhe-WI\r\nzg-he\r\npj-fs\r\nstart-RW", "226")]
        [TestCase(12, 2, "dc-end\r\nHN-start\r\nstart-kj\r\ndc-start\r\ndc-HN\r\nLN-dc\r\nHN-end\r\nkj-sa\r\nkj-HN\r\nkj-dc", "103")]
        [TestCase(12, 2, "fs-end\r\nhe-DX\r\nfs-he\r\nstart-DX\r\npj-DX\r\nend-zg\r\nzg-sl\r\nzg-pj\r\npj-he\r\nRW-he\r\nfs-DX\r\npj-RW\r\nzg-RW\r\nstart-pj\r\nhe-WI\r\nzg-he\r\npj-fs\r\nstart-RW", "3509")]
        [TestCase(13, 1, "6,10\r\n0,14\r\n9,10\r\n0,3\r\n10,4\r\n4,11\r\n6,0\r\n6,12\r\n4,1\r\n0,13\r\n10,12\r\n3,4\r\n3,0\r\n8,4\r\n1,10\r\n2,14\r\n8,10\r\n9,0\r\n\r\nfold along y=7\r\nfold along x=5", "17")]
        [TestCase(14, 1, "NNCB\r\n\r\nCH->B\r\nHH->N\r\nCB->H\r\nNH->C\r\nHB->C\r\nHC->B\r\nHN->C\r\nNN->C\r\nBH->H\r\nNC->B\r\nNB->B\r\nBN->B\r\nBB->N\r\nBC->B\r\nCC->N\r\nCN->C", "1588")]
        [TestCase(14, 2, "NNCB\r\n\r\nCH->B\r\nHH->N\r\nCB->H\r\nNH->C\r\nHB->C\r\nHC->B\r\nHN->C\r\nNN->C\r\nBH->H\r\nNC->B\r\nNB->B\r\nBN->B\r\nBB->N\r\nBC->B\r\nCC->N\r\nCN->C", "2188189693529")]
        [TestCase(15, 1, "1163751742\r\n1381373672\r\n2136511328\r\n3694931569\r\n7463417111\r\n1319128137\r\n1359912421\r\n3125421639\r\n1293138521\r\n2311944581", "40")]
        [TestCase(15, 2, "1163751742\r\n1381373672\r\n2136511328\r\n3694931569\r\n7463417111\r\n1319128137\r\n1359912421\r\n3125421639\r\n1293138521\r\n2311944581", "315")]
        [TestCase(16, 1, "8A004A801A8002F478", "16")]
        [TestCase(16, 1, "620080001611562C8802118E34", "12")]
        [TestCase(16, 1, "C0015000016115A2E0802F182340", "23")]
        [TestCase(16, 1, "A0016C880162017C3686B18A3D4780", "31")]
        [TestCase(16, 2, "C200B40A82", "3")]
        [TestCase(16, 2, "04005AC33890", "54")]
        [TestCase(16, 2, "880086C3E88112", "7")]
        [TestCase(16, 2, "CE00C43D881120", "9")]
        [TestCase(16, 2, "D8005AC2A8F0", "1")]
        [TestCase(16, 2, "F600BC2D8F", "0")]
        [TestCase(16, 2, "9C005AC2F8F0", "0")]
        [TestCase(16, 2, "9C0141080250320F1802104A08", "1")]
        [TestCase(17, 1, "target area: x=20..30, y=-10..-5", "45")]
        [TestCase(17, 2, "target area: x=20..30, y=-10..-5", "112")]
        [TestCase(18, 1, "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]\r\n[[[5,[2, 8]], 4],[5,[[9,9],0]]]\r\n[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]\r\n[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]\r\n[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]\r\n[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]\r\n[[[[5,4],[7,7]],8],[[8,3],8]]\r\n[[9,3],[[9,9],[6,[4,9]]]]\r\n[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]\r\n[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]", "4140")]
        [TestCase(18, 2, "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]\r\n[[[5,[2, 8]], 4],[5,[[9,9],0]]]\r\n[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]\r\n[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]\r\n[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]\r\n[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]\r\n[[[[5,4],[7,7]],8],[[8,3],8]]\r\n[[9,3],[[9,9],[6,[4,9]]]]\r\n[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]\r\n[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]", "3993")]
        [TestCase(20, 1, "..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#\r\n\r\n#..#.\r\n#....\r\n##..#\r\n..#..\r\n..###", "35")]
        [TestCase(20, 2, "..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#\r\n\r\n#..#.\r\n#....\r\n##..#\r\n..#..\r\n..###", "3351")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(2021, day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}