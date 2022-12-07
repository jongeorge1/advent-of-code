﻿namespace AdventOfCode.Year2021.Day22
{
    using System.Linq;
    using NUnit.Framework;

    public class RegionTests
    {
        [TestCase(0, 0, 0, 0, 0, 0, 1)]
        [TestCase(2, 4, 2, 4, 2, 4, 27)]
        [TestCase(-2, 2, -2, 2, -2, 2, 125)]
        [TestCase(-4, -2, -4, -2, -4, -2, 27)]
        public void VolumeIsCalculatedCorrectly(int minX, int maxX, int minY, int maxY, int minZ, int maxZ, long expectedVolume)
        {
            var region = new Region(minX, maxX, minY, maxY, minZ, maxZ);

            Assert.AreEqual(expectedVolume, region.Volume);
        }

        [TestCase("No intersection", 0, 2, 0, 2, 0, 2, 3, 5, 3, 5, 3, 5, false)]
        [TestCase("One corner touching", 0, 2, 0, 2, 0, 2, 2, 5, 2, 5, 2, 5, true)]
        [TestCase("Fully contained (1 in 2)", 0, 2, 0, 2, 0, 2, -2, 5, -2, 5, -2, 5, true)]
        [TestCase("Fully contained (2 in 1)", -2, 5, -2, 5, -2, 5, 0, 2, 0, 2, 0, 2, true)]
        [TestCase("Identical regions", 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, true)]
        public void IntersectionsCorrectlyDetected(string name, int minX1, int maxX1, int minY1, int maxY1, int minZ1, int maxZ1, int minX2, int maxX2, int minY2, int maxY2, int minZ2, int maxZ2, bool shouldIntersect)
        {
            var region1 = new Region(minX1, maxX1, minY1, maxY1, minZ1, maxZ1);

            var region2 = new Region(minX2, maxX2, minY2, maxY2, minZ2, maxZ2);

            Assert.AreEqual(shouldIntersect, region1.Intersects(region2));
        }

        [Test]
        public void RemoveIntersection_FullContainment()
        {
            var region1 = new Region(2, 5, 2, 5, 2, 5);
            var region2 = new Region(0, 10, 0, 10, 0, 10);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.IsEmpty(result);
        }

        [Test]
        public void RemoveIntersection_NoIntersection()
        {
            var region1 = new Region(2, 5, 2, 5, 2, 5);
            var region2 = new Region(6, 10, 6, 10, 6, 10);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(1, result.Count);

            var resultRegion = result[0];

            Assert.AreEqual(region1, resultRegion);
        }

        [Test]
        public void RemoveIntersection_SingleSliceInXAxisFromMinimum()
        {
            var region1 = new Region(2, 5, 2, 5, 2, 5);
            var region2 = new Region(0, 3, 0, 10, 0, 10);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(1, result.Count);

            var resultRegion = result[0];

            var expectedResult = new Region(4, 5, 2, 5, 2, 5);

            Assert.AreEqual(expectedResult, resultRegion);
        }

        [Test]
        public void RemoveIntersection_SingleSliceInXAxisFromMaximum()
        {
            var region1 = new Region(2, 5, 2, 5, 2, 5);
            var region2 = new Region(4, 7, 0, 10, 0, 10);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(1, result.Count);

            var resultRegion = result[0];

            var expectedResult = new Region(2, 3, 2, 5, 2, 5);

            Assert.AreEqual(expectedResult, resultRegion);
        }

        [Test]
        public void RemoveIntersection_SingleSliceInXAxisFromMiddle()
        {
            var region1 = new Region(1, 6, 2, 5, 2, 5);
            var region2 = new Region(3, 4, 0, 10, 0, 10);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.Contains(new Region(1, 2, 2, 5, 2, 5), result);
            Assert.Contains(new Region(5, 6, 2, 5, 2, 5), result);
        }

        [Test]
        public void RemoveIntersection_SingleSliceInYAxisFromMinimum()
        {
            var region1 = new Region(2, 5, 2, 5, 2, 5);
            var region2 = new Region(0, 10, 0, 3, 0, 10);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(1, result.Count);

            var resultRegion = result[0];

            var expectedResult = new Region(2, 5, 4, 5, 2, 5);

            Assert.AreEqual(expectedResult, resultRegion);
        }

        [Test]
        public void RemoveIntersection_SingleSliceInYAxisFromMaximum()
        {
            var region1 = new Region(2, 5, 2, 5, 2, 5);
            var region2 = new Region(0, 10, 4, 7, 0, 10);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(1, result.Count);

            var resultRegion = result[0];

            var expectedResult = new Region(2, 5, 2, 3, 2, 5);

            Assert.AreEqual(expectedResult, resultRegion);
        }

        [Test]
        public void RemoveIntersection_SingleSliceInYAxisFromMiddle()
        {
            var region1 = new Region(2, 5, 1, 6, 2, 5);
            var region2 = new Region(0, 10, 3, 4, 0, 10);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.Contains(new Region(2, 5, 1, 2, 2, 5), result);
            Assert.Contains(new Region(2, 5, 5, 6, 2, 5), result);
        }

        [Test]
        public void RemoveIntersection_SingleSliceInZAxisFromMinimum()
        {
            var region1 = new Region(2, 5, 2, 5, 2, 5);
            var region2 = new Region(0, 10, 0, 10, 0, 3);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(1, result.Count);

            var resultRegion = result[0];

            var expectedResult = new Region(2, 5, 2, 5, 4, 5);

            Assert.AreEqual(expectedResult, resultRegion);
        }

        [Test]
        public void RemoveIntersection_SingleSliceInZAxisFromMaximum()
        {
            var region1 = new Region(2, 5, 2, 5, 2, 5);
            var region2 = new Region(0, 10, 0, 10, 4, 7);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(1, result.Count);

            var resultRegion = result[0];

            var expectedResult = new Region(2, 5, 2, 5, 2, 3);

            Assert.AreEqual(expectedResult, resultRegion);
        }

        [Test]
        public void RemoveIntersection_SingleSliceInZAxisFromMiddle()
        {
            var region1 = new Region(2, 5, 2, 5, 1, 6);
            var region2 = new Region(0, 10, 0, 10, 3, 4);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.Contains(new Region(2, 5, 2, 5, 1, 2), result);
            Assert.Contains(new Region(2, 5, 2, 5, 5, 6), result);
        }

        [Test]
        public void RemoveIntersection_PartialIntersectionInThreeAxesFromMinimums()
        {
            var region1 = new Region(5, 10, 5, 10, 5, 10);
            var region2 = new Region(2, 8, 2, 8, 2, 8);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(152, result.Sum(x => x.Volume));
        }

        [Test]
        public void RemoveIntersection_PartialIntersectionTunnelThroughRegion()
        {
            var region1 = new Region(2, 10, 2, 10, 2, 10);
            var region2 = new Region(5, 7, 5, 7, 1, 11);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(648, result.Sum(x => x.Volume));
        }

        [Test]
        public void RemoveIntersection_MinimumEdgesAligned()
        {
            var region1 = new Region(2, 10, 2, 10, 2, 10);
            var region2 = new Region(2, 5, 2, 5, 2, 5);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(665, result.Sum(x => x.Volume));
        }

        [Test]
        public void RemoveIntersection_MaximumEdgesAligned()
        {
            var region1 = new Region(2, 10, 2, 10, 2, 10);
            var region2 = new Region(7, 10, 7, 10, 7, 10);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(665, result.Sum(x => x.Volume));
        }

        [Test]
        public void RemoveIntersection_HoleInMiddle()
        {
            var region1 = new Region(2, 10, 2, 10, 2, 10);
            var region2 = new Region(4, 7, 4, 7, 4, 7);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(665, result.Sum(x => x.Volume));
        }

        [Test]
        public void RemoveIntersection_LargeHoleInMiddle()
        {
            var region1 = new Region(2, 10, 2, 10, 2, 10);
            var region2 = new Region(3, 9, 3, 9, 3, 9);

            var result = region1.RemoveIntersection(region2).ToList();

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(386, result.Sum(x => x.Volume));
        }
    }
}
