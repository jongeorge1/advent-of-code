namespace AdventOfCode.Year2022.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Directory
    {
        private int? totalSize = null;
        private string? name;

        public IList<Directory> ChildDirectories { get; } = [];

        public string Name
        {
            get => this.name ?? throw new InvalidOperationException();
            set => this.name = value;
        }

        public Directory? ParentDirectory { get; set; }

        public int FileSizes { get; set; }

        public int GetTotalSize()
        {
            this.totalSize ??= this.FileSizes + this.ChildDirectories.Sum(x => x.GetTotalSize());

            return this.totalSize.Value;
        }

        public Directory GetChildDirectory(string name)
        {
            return this.ChildDirectories.FirstOrDefault(x => x.Name == name) ?? throw new Exception($"Attempted to get child directory with name '{name}' from directory '{this.Name}', but the directory could not be found.");
        }
    }
}
