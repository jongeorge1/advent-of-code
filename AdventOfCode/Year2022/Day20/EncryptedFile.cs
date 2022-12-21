namespace AdventOfCode.Year2022.Day20
{
    using AdventOfCode.Helpers;

    public class EncryptedFile
    {
        public EncryptedFile(string input)
        {
            // Going to try for part 1 with a linked list that has two sets of links; the original and the new.
            var numbers = new EncryptedFileListItem[5000];

            int itemCount = 0;

            foreach (StringExtensions.StringSplitEntry current in input.SplitLines())
            {
                var newItem = new EncryptedFileListItem
                {
                    Number = int.Parse(current),
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

            numbers = numbers[..itemCount];

            // Join the start and end items.
            numbers[0].AdjustedPrevious = numbers[^1];
            numbers[^1].AdjustedNext = numbers[0];

            this.ListItems = numbers;
        }

        public EncryptedFileListItem[] ListItems { get; }

        public EncryptedFileListItem Zero { get; }

        public int ReadAndSumCoordinates()
        {
            int coordinate = 0;

            EncryptedFileListItem currentCoordinateItem = this.Zero;

            for (int i = 0; i < 3; ++i)
            {
                currentCoordinateItem = currentCoordinateItem.Move(1000);
                coordinate += currentCoordinateItem.Number;
            }

            return coordinate;
        }
    }
}
