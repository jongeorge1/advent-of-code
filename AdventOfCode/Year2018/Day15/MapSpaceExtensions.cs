namespace AdventOfCode.Year2018.Day15
{
    public static class MapSpaceExtensions
    {
        public static bool IsEmpty(this MapSpace space)
        {
            return space.Unit == null;
        }
    }
}
