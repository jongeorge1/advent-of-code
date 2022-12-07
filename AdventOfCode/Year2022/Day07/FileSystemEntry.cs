namespace AdventOfCode.Year2022.Day07
{
    public abstract class FileSystemEntry
    {
        public string Name { get; set; }

        public Directory? ParentDirectory { get; set; }

        public abstract int GetSize();
    }
}
