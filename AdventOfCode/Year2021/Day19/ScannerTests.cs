namespace AdventOfCode.Year2021.Day19;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ScannerTests
{
    [TestMethod]
    public void ParsingInput()
    {
        string input = @"--- scanner 0 ---
404,-588,-901
528,-643,409
-838,591,734
390,-675,-793
-537,-823,-458
-485,-357,347
-345,-311,381
-661,-816,-575
-876,649,763
-618,-824,-621
553,345,-567
474,580,667
-447,-329,318
-584,868,-557
544,-627,-890
564,392,-477
455,729,728
-892,524,684
-689,845,-530
423,-701,434
7,-33,-71
630,319,-379
443,580,662
-789,900,-551
459,-707,401";

        var scanner = new Scanner(input);

        Assert.AreEqual(0, scanner.Number);

        Assert.AreEqual(25, scanner.Beacons.Length);
        Assert.AreEqual(528, scanner.Beacons[1].X);
        Assert.AreEqual(-643, scanner.Beacons[1].Y);
        Assert.AreEqual(409, scanner.Beacons[1].Z);
    }

    [TestMethod]
    public void OverlapDetection()
    {
        var scanner1 = new Scanner(@"--- scanner 0 ---
404,-588,-901
528,-643,409
-838,591,734
390,-675,-793
-537,-823,-458
-485,-357,347
-345,-311,381
-661,-816,-575
-876,649,763
-618,-824,-621
553,345,-567
474,580,667
-447,-329,318
-584,868,-557
544,-627,-890
564,392,-477
455,729,728
-892,524,684
-689,845,-530
423,-701,434
7,-33,-71
630,319,-379
443,580,662
-789,900,-551
459,-707,401");
        var scanner2 = new Scanner(@"--- scanner 1 ---
686,422,578
605,423,415
515,917,-361
-336,658,858
95,138,22
-476,619,847
-340,-569,-846
567,-361,727
-460,603,-452
669,-402,600
729,430,532
-500,-761,534
-322,571,750
-466,-666,-811
-429,-592,574
-355,545,-477
703,-491,-529
-328,-685,520
413,935,-424
-391,539,-444
586,-435,557
-364,-763,-893
807,-499,-711
755,-354,-619
553,889,-390");

        scanner1.Position = (0, 0, 0);
        scanner1.TransformationRequiredToBeRelativeToOrigin = x => x;

        bool overlaps = scanner1.TryUpdateRelativePositionOf(scanner2);

        Assert.IsTrue(overlaps);
        Assert.AreEqual((68, -1246, -43), scanner2.Position!.Value);
    }

    [TestMethod]
    public void MultipleOverlapDetection()
    {
        var scanner0 = new Scanner(@"--- scanner 0 ---
404,-588,-901
528,-643,409
-838,591,734
390,-675,-793
-537,-823,-458
-485,-357,347
-345,-311,381
-661,-816,-575
-876,649,763
-618,-824,-621
553,345,-567
474,580,667
-447,-329,318
-584,868,-557
544,-627,-890
564,392,-477
455,729,728
-892,524,684
-689,845,-530
423,-701,434
7,-33,-71
630,319,-379
443,580,662
-789,900,-551
459,-707,401");
        var scanner1 = new Scanner(@"--- scanner 1 ---
686,422,578
605,423,415
515,917,-361
-336,658,858
95,138,22
-476,619,847
-340,-569,-846
567,-361,727
-460,603,-452
669,-402,600
729,430,532
-500,-761,534
-322,571,750
-466,-666,-811
-429,-592,574
-355,545,-477
703,-491,-529
-328,-685,520
413,935,-424
-391,539,-444
586,-435,557
-364,-763,-893
807,-499,-711
755,-354,-619
553,889,-390");

        var scanner4 = new Scanner(@"--- scanner 4 ---
727,592,562
-293,-554,779
441,611,-461
-714,465,-776
-743,427,-804
-660,-479,-426
832,-632,460
927,-485,-438
408,393,-506
466,436,-512
110,16,151
-258,-428,682
-393,719,612
-211,-452,876
808,-476,-593
-575,615,604
-485,667,467
-680,325,-822
-627,-443,-432
872,-547,-609
833,512,582
807,604,487
839,-516,451
891,-625,532
-652,-548,-490
30,-46,-14");

        scanner0.Position = (0, 0, 0);
        scanner0.TransformationRequiredToBeRelativeToOrigin = x => x;

        bool overlaps = scanner0.TryUpdateRelativePositionOf(scanner1);
        Assert.IsTrue(overlaps);
        Assert.AreEqual((68, -1246, -43), scanner1.Position!.Value);

        overlaps = scanner0.TryUpdateRelativePositionOf(scanner4);
        Assert.IsFalse(overlaps);
        Assert.AreEqual((68, -1246, -43), scanner1.Position.Value);

        overlaps = scanner1.TryUpdateRelativePositionOf(scanner4);
        Assert.IsTrue(overlaps);
        Assert.AreEqual((68, -1246, -43), scanner1.Position.Value);
        Assert.AreEqual((-20, -1133, 1061), scanner4.Position!.Value);
    }
}
