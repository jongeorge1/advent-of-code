namespace AdventOfCode.Year2022.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Directory : FileSystemEntry
    {
        private int? size = null;

        public IList<FileSystemEntry> Children { get; } = new List<FileSystemEntry>();

        public override int GetSize()
        {
            this.size ??= this.Children.Sum(x => x.GetSize());

            return this.size.Value;
        }

        public Directory GetChildDirectory(string name)
        {
            return this.Children.FirstOrDefault(x => x.Name == name) as Directory ?? throw new Exception($"Attempted to get child directory with name '{name}' from directory '{this.Name}', but the directory could not be found.");
        }
    }
}
