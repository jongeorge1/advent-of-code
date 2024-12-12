namespace AdventOfCode.Year2024.Day09;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        FileSystemEntry[] fileSystemEntries = input[0].Chunk(2).SelectMany((input, index) =>
        {
            var file = new FileSystemEntry(input[0] - '0', index);

            if (input.Length == 2)
            {
                return
                (FileSystemEntry[])[
                    file,
                    new FileSystemEntry(input[1] - '0', null),
                ];
            }
            else
            {
                return [file];
            }
        }).ToArray();

        LinkedList<FileSystemEntry> fileSystem = new(fileSystemEntries);

        List<FileSystemEntry> processedEntries = [];
        LinkedListNode<FileSystemEntry>? targetItem = fileSystem.Last;

        while (targetItem is not null)
        {
            // Only need to do something if
            // a) the item is a file (i.e. has an ID)
            // b) the item hasn't been processed yet. It's possible we'll encounter the same item more than once as we work back through the queue.
            if (targetItem.Value.Id.HasValue && !processedEntries.Contains(targetItem.Value))
            {
                // We need to find the first gap big enough for the file.
                LinkedListNode<FileSystemEntry>? firstGap = fileSystem.FindBefore(targetItem, el => !el.Id.HasValue && el.Size >= targetItem.Value.Size);

                if (firstGap is not null)
                {
                    // We might need to break this gap into two parts.
                    int difference = firstGap.Value.Size - targetItem.Value.Size;
                    if (difference > 0)
                    {
                        LinkedListNode<FileSystemEntry>[] replacementNodes = firstGap.Replace(
                            new FileSystemEntry(targetItem.Value.Size, null),
                            new FileSystemEntry(difference, null));

                        firstGap = replacementNodes[0];
                    }

                    // Now swap the nodes
                    fileSystem.SwapNodes(firstGap, targetItem);

                    processedEntries.Add(targetItem.Value);

                    // Now the gap node is where our target item was.
                    targetItem = firstGap;
                }
            }

            targetItem = targetItem.Previous;

            ////Console.WriteLine(Format(fileSystem));
        }

        long checksum = 0;
        int location = 0;
        foreach (FileSystemEntry fileSystemEntry in fileSystem)
        {
            if (fileSystemEntry.Id.HasValue)
            {
                for (int offset = 0; offset < fileSystemEntry.Size; ++offset)
                {
                    checksum += fileSystemEntry.Id!.Value * (location + offset);
                }
            }

            location += fileSystemEntry.Size;
        }

        return checksum.ToString();
    }

    private static string Format(LinkedList<FileSystemEntry> fileSystem)
    {
        StringBuilder builder = new();

        LinkedListNode<FileSystemEntry>? current = fileSystem.First;

        while (current is not null)
        {
            if (current.Value.Id.HasValue)
            {
                builder.Append(new string((char)(current.Value.Id + '0'), current.Value.Size));
            }
            else
            {
                builder.Append(new string('.', current.Value.Size));
            }

            current = current.Next;
        }

        return builder.ToString();
    }

    public readonly record struct FileSystemEntry(int Size, int? Id);
}
