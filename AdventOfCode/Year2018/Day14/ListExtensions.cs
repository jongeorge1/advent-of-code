namespace AdventOfCode.Year2018.Day14
{
    using System.Collections.Generic;

    public static class ListExtensions
    {
        public static int FindIndex(this List<int> source, List<int> target, int start)
        {
            int lastIndexToCheck = source.Count - target.Count;

            if (lastIndexToCheck < 0 || lastIndexToCheck < start)
            {
                return -1;
            }

            for (int index = start; index <= lastIndexToCheck; index++)
            {
                bool matches = true;

                for (int offset = 0; offset < target.Count; offset++)
                {
                    if (source[index + offset] != target[offset])
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                {
                    return index;
                }
            }

            return -1;
        }
    }
}
