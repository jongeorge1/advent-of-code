namespace AdventOfCode.Year2021
{
    using System;
    using AdventOfCode;
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
        [TestCase(21, 1, "Player 1 starting position: 4\r\nPlayer 2 starting position: 8", "739785")]
        [TestCase(21, 2, "Player 1 starting position: 4\r\nPlayer 2 starting position: 8", "444356092776315")]
        [TestCase(22, 1, "on x=10..12,y=10..12,z=10..12\r\non x=11..13,y=11..13,z=11..13\r\noff x =9..11,y=9..11,z=9..11\r\non x =10..10,y=10..10,z=10..10", "39")]
        [TestCase(22, 1, "on x=-20..26,y=-36..17,z=-47..7\r\non x=-20..33,y=-21..23,z=-26..28\r\non x=-22..28,y=-29..23,z=-38..16\r\non x=-46..7,y=-6..46,z=-50..-1\r\non x=-49..1,y=-3..46,z=-24..28\r\non x=2..47,y=-22..22,z=-23..27\r\non x=-27..23,y=-28..26,z=-21..29\r\non x=-39..5,y=-6..47,z=-3..44\r\non x=-30..21,y=-8..43,z=-13..34\r\non x=-22..26,y=-27..20,z=-29..19\r\noff x=-48..-32,y=26..41,z=-47..-37\r\non x=-12..35,y=6..50,z=-50..-2\r\noff x=-48..-32,y=-32..-16,z=-15..-5\r\non x=-18..26,y=-33..15,z=-7..46\r\noff x=-40..-22,y=-38..-28,z=23..41\r\non x=-16..35,y=-41..10,z=-47..6\r\noff x=-32..-23,y=11..30,z=-14..3\r\non x=-49..-5,y=-3..45,z=-29..18\r\noff x=18..30,y=-20..-8,z=-3..13\r\non x=-41..9,y=-7..43,z=-33..15\r\non x=-54112..-39298,y=-85059..-49293,z=-27449..7877\r\non x=967..23432,y=45373..81175,z=27513..53682", "590784")]
        [TestCase(22, 2, "on x=-5..47,y=-31..22,z=-19..33\r\non x=-44..5,y=-27..21,z=-14..35\r\non x=-49..-1,y=-11..42,z=-10..38\r\non x=-20..34,y=-40..6,z=-44..1\r\noff x=26..39,y=40..50,z=-2..11\r\non x=-41..5,y=-41..6,z=-36..8\r\noff x=-43..-33,y=-45..-28,z=7..25\r\non x=-33..15,y=-32..19,z=-34..11\r\noff x=35..47,y=-46..-34,z=-11..5\r\non x=-14..36,y=-6..44,z=-16..29\r\non x=-57795..-6158,y=29564..72030,z=20435..90618\r\non x=36731..105352,y=-21140..28532,z=16094..90401\r\non x=30999..107136,y=-53464..15513,z=8553..71215\r\non x=13528..83982,y=-99403..-27377,z=-24141..23996\r\non x=-72682..-12347,y=18159..111354,z=7391..80950\r\non x=-1060..80757,y=-65301..-20884,z=-103788..-16709\r\non x=-83015..-9461,y=-72160..-8347,z=-81239..-26856\r\non x=-52752..22273,y=-49450..9096,z=54442..119054\r\non x=-29982..40483,y=-108474..-28371,z=-24328..38471\r\non x=-4958..62750,y=40422..118853,z=-7672..65583\r\non x=55694..108686,y=-43367..46958,z=-26781..48729\r\non x=-98497..-18186,y=-63569..3412,z=1232..88485\r\non x=-726..56291,y=-62629..13224,z=18033..85226\r\non x=-110886..-34664,y=-81338..-8658,z=8914..63723\r\non x=-55829..24974,y=-16897..54165,z=-121762..-28058\r\non x=-65152..-11147,y=22489..91432,z=-58782..1780\r\non x=-120100..-32970,y=-46592..27473,z=-11695..61039\r\non x=-18631..37533,y=-124565..-50804,z=-35667..28308\r\non x=-57817..18248,y=49321..117703,z=5745..55881\r\non x=14781..98692,y=-1341..70827,z=15753..70151\r\non x=-34419..55919,y=-19626..40991,z=39015..114138\r\non x=-60785..11593,y=-56135..2999,z=-95368..-26915\r\non x=-32178..58085,y=17647..101866,z=-91405..-8878\r\non x=-53655..12091,y=50097..105568,z=-75335..-4862\r\non x=-111166..-40997,y=-71714..2688,z=5609..50954\r\non x=-16602..70118,y=-98693..-44401,z=5197..76897\r\non x=16383..101554,y=4615..83635,z=-44907..18747\r\noff x=-95822..-15171,y=-19987..48940,z=10804..104439\r\non x=-89813..-14614,y=16069..88491,z=-3297..45228\r\non x=41075..99376,y=-20427..49978,z=-52012..13762\r\non x=-21330..50085,y=-17944..62733,z=-112280..-30197\r\non x=-16478..35915,y=36008..118594,z=-7885..47086\r\noff x=-98156..-27851,y=-49952..43171,z=-99005..-8456\r\noff x=2032..69770,y=-71013..4824,z=7471..94418\r\non x=43670..120875,y=-42068..12382,z=-24787..38892\r\noff x=37514..111226,y=-45862..25743,z=-16714..54663\r\noff x=25699..97951,y=-30668..59918,z=-15349..69697\r\noff x=-44271..17935,y=-9516..60759,z=49131..112598\r\non x=-61695..-5813,y=40978..94975,z=8655..80240\r\noff x=-101086..-9439,y=-7088..67543,z=33935..83858\r\noff x=18020..114017,y=-48931..32606,z=21474..89843\r\noff x=-77139..10506,y=-89994..-18797,z=-80..59318\r\noff x=8476..79288,y=-75520..11602,z=-96624..-24783\r\non x=-47488..-1262,y=24338..100707,z=16292..72967\r\noff x=-84341..13987,y=2429..92914,z=-90671..-1318\r\noff x=-37810..49457,y=-71013..-7894,z=-105357..-13188\r\noff x=-27365..46395,y=31009..98017,z=15428..76570\r\noff x=-70369..-16548,y=22648..78696,z=-1892..86821\r\non x=-53470..21291,y=-120233..-33476,z=-44150..38147\r\noff x=-93533..-4276,y=-16170..68771,z=-104985..-24507", "2758514936282235")]
        [TestCase(23, 1, "#############\r\n#...........#\r\n###B#C#B#D###\r\n  #A#D#C#A#\r\n  #########", "12521")]
        [TestCase(23, 2, "#############\r\n#...........#\r\n###B#C#B#D###\r\n  #A#D#C#A#\r\n  #########", "44169")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(2021, day, part);
            string result = solution.Solve(input.Split(Environment.NewLine));
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}