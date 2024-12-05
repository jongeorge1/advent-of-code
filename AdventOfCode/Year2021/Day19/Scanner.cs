namespace AdventOfCode.Year2021.Day19
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Scanner
    {
        public static readonly Func<(int X, int Y, int Z), (int X, int Y, int Z)>[] ScannerDirectionTransforms = new Func<(int X, int Y, int Z), (int X, int Y, int Z)>[]
        {
            input => (input.X, input.Y, input.Z),
            input => (input.X, -input.Y, -input.Z),
            input => (input.X, -input.Z, input.Y),
            input => (-input.Y, -input.Z, input.X),
            input => (input.Y, -input.Z, -input.X),
            input => (-input.X, -input.Z, -input.Y),
        };

        public static readonly Func<(int X, int Y, int Z), (int X, int Y, int Z)>[] ScannerRotationTransforms = new Func<(int X, int Y, int Z), (int X, int Y, int Z)>[]
        {
            input => (input.X, input.Y, input.Z),
            input => (-input.Y, input.X, input.Z),
            input => (-input.X, -input.Y, input.Z),
            input => (input.Y, -input.X, input.Z),
        };

        public static readonly Func<(int X, int Y, int Z), (int X, int Y, int Z)>[] ScannerAllTransforms;

        static Scanner()
        {
            ScannerAllTransforms = ScannerDirectionTransforms.SelectMany(
                directionTransform => ScannerRotationTransforms.Select<Func<(int X, int Y, int Z), (int X, int Y, int Z)>, Func<(int X, int Y, int Z), (int X, int Y, int Z)>>(
                    rotationTransform => ((int, int, int) input) => rotationTransform(directionTransform(input)))).ToArray();
        }

        public Scanner(string input)
        {
            string[] rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            this.Number = int.Parse(rows[0].Split(' ')[2]);

            this.Beacons = rows[1..].Select(x => x.Split(',')).Select(y => (int.Parse(y[0]), int.Parse(y[1]), int.Parse(y[2]))).ToArray();

            // To determine overlaps we need to build a number of copies of the beacon sets in which we express all other beacons relative
            // to a single beacon.
            this.BeaconRelativePositions = this.Beacons.ToDictionary(x => x, x => this.Beacons.Select(b => (b.X - x.X, b.Y - x.Y, b.Z - x.Z)).ToArray());
        }

        public int Number { get; }

        public (int X, int Y, int Z)[] Beacons { get; }

        public (int X, int Y, int Z)? Position { get; set; }

        public Func<(int X, int Y, int Z), (int X, int Y, int Z)>? TransformationRequiredToBeRelativeToOrigin { get; set; }

        public bool PositionKnown => this.Position.HasValue;

        public Dictionary<(int X, int Y, int Z), (int, int, int)[]> BeaconRelativePositions { get; }

        public static void ResolveAllPositionsRelativeToFirst(Scanner[] scanners)
        {
            scanners[0].Position = (0, 0, 0);
            scanners[0].TransformationRequiredToBeRelativeToOrigin = x => x;

            var locatedQueue = new Queue<Scanner>();
            locatedQueue.Enqueue(scanners[0]);

            while (locatedQueue.Count > 0)
            {
                Scanner current = locatedQueue.Dequeue();

                ////Console.WriteLine($"Looking for neighbours of scanner {current.Number}");

                foreach (Scanner unknownPositionedScanner in scanners.Where(x => !x.PositionKnown))
                {
                    if (current.TryUpdateRelativePositionOf(unknownPositionedScanner))
                    {
                        ////Console.WriteLine($"    Scanner {unknownPositionedScanner.Number} is a neighbour");
                        locatedQueue.Enqueue(unknownPositionedScanner);
                    }
                    ////else
                    ////{
                    ////    Console.WriteLine($"    Scanner {unknownPositionedScanner.Number} is not a neighbour");
                    ////}
                }
            }
        }

        public IEnumerable<(int X, int Y, int Z)> GetBeaconsWithAbsolutePositions()
        {
            if (!this.PositionKnown)
            {
                throw new Exception("I don't know where I am!");
            }

            foreach ((int X, int Y, int Z) beacon in this.Beacons)
            {
                (int X, int Y, int Z) transformed = this.TransformationRequiredToBeRelativeToOrigin!(beacon);
                yield return (this.Position!.Value.X + transformed.X, this.Position.Value.Y + transformed.Y, this.Position.Value.Z + transformed.Z);
            }
        }

        public bool TryUpdateRelativePositionOf(Scanner targetScanner)
        {
            if (!this.PositionKnown)
            {
                throw new Exception("Can't find relative position to this scanner as we don't know our own position yet.");
            }

            if (targetScanner.PositionKnown)
            {
                throw new Exception("The position of the target scanner is already known.");
            }

            foreach (Func<(int X, int Y, int Z), (int X, int Y, int Z)> transform in ScannerAllTransforms)
            {
                if (this.TryUpdateRelativePositionWithTransform(targetScanner, transform))
                {
                    return true;
                }
            }

            return false;
        }

        private bool TryUpdateRelativePositionWithTransform(Scanner targetScanner, Func<(int X, int Y, int Z), (int X, int Y, int Z)> transformToTest)
        {
            // Now we need to determine if there are 12 beacons that look the same when the transform is applied to the
            // incoming scanner.
            (int X, int Y, int Z)[] transformedBeacons = targetScanner.Beacons.Select(transformToTest).ToArray();

            // To find out whether a set of beacons overlap we need to transform them into a common coordinate system.
            // Because we don't know the origin (scanner location), we'll have to choose one of the beacons for each set
            // to be the origin. We then transform the other locations and see if any match. But because we don't know
            // which beacons are common, we'll have to try with all of them.
            foreach ((int X, int Y, int Z) beacon in this.Beacons)
            {
                (int, int, int)[] source = this.BeaconRelativePositions[beacon];

                foreach (KeyValuePair<(int X, int Y, int Z), (int, int, int)[]> targetSet in targetScanner.BeaconRelativePositions)
                {
                    IEnumerable<(int, int, int)> matchingBeacons = source.Intersect(targetSet.Value.Select(transformToTest));
                    if (matchingBeacons.Count() >= 12)
                    {
                        // Now we can use the position of the beacon we've identified as being at our 0, 0 position in both, which are
                        // representing the same point, to establish the position of the target scanner.
                        (int X, int Y, int Z) myMatchingBeconPosition = beacon;
                        (int X, int Y, int Z) targetMatchingBeaconPosition = targetSet.Key;
                        (int X, int Y, int Z) targetMatchingBeaconPositionTransformedToMe = transformToTest(targetMatchingBeaconPosition);

                        (int, int, int) targetScannerPositionRelativeToMe = (myMatchingBeconPosition.X - targetMatchingBeaconPositionTransformedToMe.X, myMatchingBeconPosition.Y - targetMatchingBeaconPositionTransformedToMe.Y, myMatchingBeconPosition.Z - targetMatchingBeaconPositionTransformedToMe.Z);

                        // However, if we're not the origin, we need to further transform things to get to it.
                        (int X, int Y, int Z) targetScannerPositionTransformedRelativeToOrigin = this.TransformationRequiredToBeRelativeToOrigin!(targetScannerPositionRelativeToMe);
                        targetScanner.Position = (targetScannerPositionTransformedRelativeToOrigin.X + this.Position!.Value.X, targetScannerPositionTransformedRelativeToOrigin.Y + this.Position.Value.Y, targetScannerPositionTransformedRelativeToOrigin.Z + this.Position.Value.Z);
                        targetScanner.TransformationRequiredToBeRelativeToOrigin = x => this.TransformationRequiredToBeRelativeToOrigin(transformToTest(x));

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
