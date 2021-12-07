namespace AoC.Solutions.Year2016.Day15
{
    public class Disc
    {
        public Disc(string input)
        {
            string[] components = input.Split(' ');
            this.Number = int.Parse(components[1][1..]);
            this.Positions = int.Parse(components[3]);
            this.StartPosition = int.Parse(components[^1][..^1]);
        }

        public Disc(int number, int positions, int startPosition)
        {
            this.Number = number;
            this.Positions = positions;
            this.StartPosition = startPosition;
        }

        public int Number { get; }

        public int Positions { get; }

        public int StartPosition { get; }

        public int PositionAt(int t)
        {
            return (this.StartPosition + t) % this.Positions;
        }
    }
}
