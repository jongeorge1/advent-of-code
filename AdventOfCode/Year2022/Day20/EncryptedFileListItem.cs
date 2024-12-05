namespace AdventOfCode.Year2022.Day20
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EncryptedFileListItem
    {
        public long Number { get; set; }

        public long NumberOffset { get; set; }

        public EncryptedFileListItem? AdjustedNext { get; set; }

        public EncryptedFileListItem? AdjustedPrevious { get; set; }

        private string DebuggerDisplay
        {
            get => $"{this.AdjustedPrevious?.Number}, [{this.Number}], {this.AdjustedNext?.Number}";
        }

        public EncryptedFileListItem Move(long count)
        {
            return count < 0
                ? this.MoveBack(count * -1)
                : this.MoveForward(count);
        }

        public EncryptedFileListItem MoveForward(long count)
        {
            EncryptedFileListItem current = this;

            for (int i = 0; i < count; i++)
            {
                current = current.AdjustedNext ?? throw new InvalidOperationException("Cannot move forward from the end of the list.");
            }

            return current;
        }

        public EncryptedFileListItem MoveBack(long count)
        {
            EncryptedFileListItem current = this.AdjustedPrevious ?? throw new InvalidOperationException("Cannot move back from the start of the list.");

            for (int i = 0; i < count; i++)
            {
                current = current.AdjustedPrevious ?? throw new InvalidOperationException("Cannot move back from the start of the list.");
            }

            return current;
        }
    }
}
