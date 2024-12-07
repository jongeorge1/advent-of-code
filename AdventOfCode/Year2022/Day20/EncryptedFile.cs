namespace AdventOfCode.Year2022.Day20;

using System;

public class EncryptedFile
{
    private EncryptedFileListItem? zero;

    public EncryptedFile(string[] input)
    {
        // Going to try for part 1 with a linked list that has two sets of links; the original and the new.
        var numbers = new EncryptedFileListItem[input.Length];

        int itemCount = 0;

        foreach (string current in input)
        {
            var newItem = new EncryptedFileListItem
            {
                Number = long.Parse(current),
            };

            if (itemCount > 0)
            {
                newItem.AdjustedPrevious = numbers[itemCount - 1];
                numbers[itemCount - 1].AdjustedNext = newItem;
            }

            numbers[itemCount++] = newItem;

            if (newItem.Number == 0)
            {
                this.Zero = newItem;
            }
        }

        // Join the start and end items.
        numbers[0].AdjustedPrevious = numbers[^1];
        numbers[^1].AdjustedNext = numbers[0];

        this.ListItems = numbers;
    }

    public EncryptedFileListItem[] ListItems { get; }

    public EncryptedFileListItem Zero
    {
        get => this.zero ?? throw new InvalidOperationException("Zero has not been set.");
        set => this.zero = value;
    }

    public void ApplyDecryptionKey(long multiplier)
    {
        foreach (EncryptedFileListItem current in this.ListItems)
        {
            current.Number *= multiplier;
            current.NumberOffset = current.Number % (this.ListItems.Length - 1);
        }
    }

    public long ReadAndSumCoordinates()
    {
        long coordinate = 0;

        EncryptedFileListItem currentCoordinateItem = this.Zero;

        for (int i = 0; i < 3; ++i)
        {
            currentCoordinateItem = currentCoordinateItem.Move(1000);
            coordinate += currentCoordinateItem.Number;
        }

        return coordinate;
    }
}
