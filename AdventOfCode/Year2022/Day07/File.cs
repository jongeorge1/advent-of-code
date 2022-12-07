namespace AdventOfCode.Year2022.Day07
{
    public class File : FileSystemEntry
    {
        public int Size { get; set; }

        public override int GetSize() => this.Size;
    }
}
