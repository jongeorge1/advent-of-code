namespace AdventOfCode.Year2018.Day04;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TimeRecordTests
{
    [TestMethod]
    public void CreationFromStartsShiftTimeRecordString()
    {
        var result = TimeRecord.FromString("[1518-11-14 00:02] Guard #3011 begins shift");

        Assert.AreEqual(new DateTime(1518, 11, 14, 0, 2, 0), result.DateTime);
        Assert.AreEqual(TimeRecordActivity.StartsShift, result.Activity);
        Assert.AreEqual(3011, result.GuardNumber);
    }

    [TestMethod]
    public void CreationFromFallsAsleepTimeRecordString()
    {
        var result = TimeRecord.FromString("[1518-11-01 23:58] falls asleep");

        Assert.AreEqual(new DateTime(1518, 11, 01, 23, 58, 0), result.DateTime);
        Assert.AreEqual(TimeRecordActivity.FallsAsleep, result.Activity);
        Assert.IsNull(result.GuardNumber);
    }

    [TestMethod]
    public void CreationFromWakesUpTimeRecordString()
    {
        var result = TimeRecord.FromString("[1518-06-09 00:52] wakes up");

        Assert.AreEqual(new DateTime(1518, 06, 09, 0, 52, 0), result.DateTime);
        Assert.AreEqual(TimeRecordActivity.WakesUp, result.Activity);
        Assert.IsNull(result.GuardNumber);
    }
}
