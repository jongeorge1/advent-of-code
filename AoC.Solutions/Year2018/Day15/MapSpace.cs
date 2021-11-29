namespace AoC.Solutions.Year2018.Day15
{
    public class MapSpace
    {
        public MapSpace(int location)
        {
            this.Location = location;
        }

        public int Location { get; set; }

        public Unit? Unit { get; set; }

        public static MapSpace? Parse(char input, int location)
        {
            var result = new MapSpace(location);

            switch (input)
            {
                case '#':
                    return null;

                case '.':
                    break;

                case 'E':
                    result.Unit = new Elf { CurrentLocation = result };
                    break;

                case 'G':
                    result.Unit = new Goblin { CurrentLocation = result };
                    break;
            }

            return result;
        }

        public override string ToString()
        {
            return this.Unit?.ToString() ?? ".";
        }
    }
}
