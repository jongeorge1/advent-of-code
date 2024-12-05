namespace AdventOfCode.Year2022.Day07
{
    using System;
    using System.Collections.Generic;

    public class FileSystem
    {
        public FileSystem(string[] input)
        {
            this.CurrentDirectory = this.Root;

            foreach (string command in input)
            {
                string[] segments = command.Split(" ");
                if (segments[0] == "$" && segments[1] == "cd")
                {
                    if (segments[2] == "/")
                    {
                        this.CurrentDirectory = this.Root;
                    }
                    else if (segments[2] == "..")
                    {
                        this.CurrentDirectory = this.CurrentDirectory?.ParentDirectory ?? throw new Exception("Attempt to move to parent folder from root");
                    }
                    else
                    {
                        this.CurrentDirectory = this.CurrentDirectory.GetChildDirectory(segments[2]);
                    }
                }
                else if (segments[0] == "$" && segments[1] == "ls")
                {
                    // Don't do anything; we can assume that any other entries we find prior to hitting the next command
                    // are files/folders being added to the current directory.
                }
                else if (segments[0] == "dir")
                {
                    var newDirectory = new Directory { Name = segments[1], ParentDirectory = this.CurrentDirectory };
                    this.CurrentDirectory.ChildDirectories.Add(newDirectory);
                    this.AllDirectories.Add(newDirectory);
                }
                else
                {
                    this.CurrentDirectory.FileSizes += int.Parse(segments[0]);
                }
            }
        }

        public Directory Root { get; } = new Directory { Name = "/" };

        public List<Directory> AllDirectories { get; } = [];

        public Directory CurrentDirectory { get; private set; }

        public int GetFreeSpace()
        {
            return 70000000 - this.Root.GetTotalSize();
        }
    }
}
