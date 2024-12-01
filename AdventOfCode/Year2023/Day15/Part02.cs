namespace AdventOfCode.Year2023.Day15
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;
    using AdventOfCode.Helpers;
    using static AdventOfCode.Helpers.StringExtensions;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            Box[] boxes = new Box[256];

            boxes.Initialize();

            // Only one line in the input, which is a comma separated string
            StringSplitEnumerator enumerator = input[0].OptimizedSplit(",");
            while (enumerator.MoveNext())
            {
                // Find the operation index
                int operationIndex = enumerator.Current.Line.IndexOf('-');
                if (operationIndex == -1)
                {
                    operationIndex = enumerator.Current.Line.IndexOf('=');
                }

                string lensLabel = enumerator.Current.Line[..operationIndex].ToString();

                int boxNumber = Hash(lensLabel);
                Box box = boxes[boxNumber];


                if (enumerator.Current.Line[operationIndex] == '-')
                {
                    box.RemoveLensIfExists(lensLabel);
                }
                else
                {
                    // It's an =, so we need to get the new focal length and then add/replace
                    int focalLength = int.Parse(enumerator.Current.Line[(operationIndex + 1)..]);
                    box.AddOrReplaceLens(lensLabel, focalLength);
                }
            }

            long runningTotal = 0;
            for (int i = 0; i < boxes.Length; ++i)
            {
                runningTotal += boxes[i].Score(i + 1);
            }

            return runningTotal.ToString();
        }

        private static int Hash(string current)
        {
            int runningTotal = 0;

            for (int i = 0; i < current.Length; ++i)
            {
                runningTotal += (int)current[i];
                runningTotal *= 17;
                runningTotal %= 256;
            }

            return runningTotal;
        }

        private readonly struct Box
        {
            public Box()
            {
            }

            public List<Lens> Lenses { get; } = [];

            public readonly long Score(int boxNumber)
            {
                long runningTotal = 0;
                for (int i = 0; i < this.Lenses.Count; ++i)
                {
                    runningTotal += boxNumber * (i + 1) * this.Lenses[i].FocalLength;
                }

                return runningTotal;
            }

            public void RemoveLensIfExists(string label)
            {
                Lens? match = this.Lenses.FirstOrDefault(x => x.Label == label);

                if (match != null)
                {
                    this.Lenses.Remove(match);
                }
            }

            public void AddOrReplaceLens(string label, int focalLength)
            {
                Lens? match = this.Lenses.FirstOrDefault(x => x.Label == label);

                if (match != null)
                {
                    int index = this.Lenses.IndexOf(match);
                    this.Lenses.RemoveAt(index);
                    this.Lenses.Insert(index, new (label, focalLength));
                }
                else
                {
                    this.Lenses.Add(new (label, focalLength));
                }

            }
        }

        private record Lens(string Label, int FocalLength)
        {
        }
    }
}
