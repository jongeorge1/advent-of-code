namespace AdventOfCode.Year2024.Day05;

using System;
using System.Linq;

public readonly struct Update(string pages)
{
    public int[] Pages { get; } = pages.Split(",").Select(int.Parse).ToArray();

    public int MiddlePageNumber => this.Pages[this.Pages.Length / 2];

    public bool IsValid(Rule[] rules)
    {
        foreach (Rule rule in rules)
        {
            int indexOfFirst = Array.IndexOf(this.Pages, rule.First);
            int indexOfSecond = Array.IndexOf(this.Pages, rule.Second);

            if (indexOfFirst != -1 && indexOfSecond != -1 && indexOfFirst > indexOfSecond)
            {
                return false;
            }
        }

        return true;
    }

    public void Reorder(Rule[] rules)
    {
        int[] pages = this.Pages;

        Array.Sort(this.Pages, (first, second) =>
        {
            if (rules.Any(rule => rule.First == first && rule.Second == second))
            {
                return -1;
            }

            if (rules.Any(rule => rule.First == second && rule.Second == first))
            {
                return 1;
            }

            return 0;
        });
    }
}
