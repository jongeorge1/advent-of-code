namespace AdventOfCode.Year2015.Day14
{
    using System;

    public class Reindeer
    {
        public Reindeer(string input)
        {
            string[] components = input.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            this.Name = components[0];
            this.Speed = int.Parse(components[3]);
            this.FlightTime = int.Parse(components[6]);
            this.RestTime = int.Parse(components[13]);
        }

        public string Name { get; }

        public int Speed { get; }

        public int FlightTime { get; }

        public int RestTime { get; }

        public int GetDistanceAfterTime(int time)
        {
            int iterationTime = this.FlightTime + this.RestTime;
            int iterationDistance = this.Speed * this.FlightTime;

            int fullIterations = (int)Math.Floor((decimal)time / iterationTime);
            int distance = fullIterations * iterationDistance;

            int remainingFlightTime = Math.Min(this.FlightTime, time % iterationTime);
            distance += remainingFlightTime * this.Speed;

            return distance;
        }
    }
}
