namespace AdventOfCode.Year2022.Day25;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SnafuConverterTests
{
    [TestMethod]
    [DataRow("1=-0-2", 1747)]
    [DataRow("12111", 906)]
    [DataRow("2=0=", 198)]
    [DataRow("21", 11)]
    [DataRow("2=01", 201)]
    [DataRow("111", 31)]
    [DataRow("20012", 1257)]
    [DataRow("112", 32)]
    [DataRow("1=-1=", 353)]
    [DataRow("1-12", 107)]
    [DataRow("12", 7)]
    [DataRow("1=", 3)]
    [DataRow("122", 37)]
    public void ConvertFromSnafuTests(string snafu, int expected)
    {
        long actual = SnafuConverter.ToLong(snafu.AsSpan());
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(1747, "1=-0-2")]
    [DataRow(906, "12111")]
    [DataRow(198, "2=0=")]
    [DataRow(11, "21")]
    [DataRow(201, "2=01")]
    [DataRow(31, "111")]
    [DataRow(1257, "20012")]
    [DataRow(32, "112")]
    [DataRow(353, "1=-1=")]
    [DataRow(107, "1-12")]
    [DataRow(7, "12")]
    [DataRow(3, "1=")]
    [DataRow(37, "122")]
    public void ConvertToSnafuTests(int number, string expected)
    {
        ReadOnlySpan<char> actual = SnafuConverter.ToSnafu(number);
        Assert.AreEqual(expected, actual.ToString());
    }
}
