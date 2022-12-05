namespace AdventOfCode.Year2018.Day15
{
    public static class LocationHelper
    {
        public static (int X, int Y) GetCoordinates(int location, int yOffset)
        {
            return (location % yOffset, location / yOffset);
        }

        public static int GetLocation(int x, int y, int yOffset)
        {
            return x + (y * yOffset);
        }
    }
}
