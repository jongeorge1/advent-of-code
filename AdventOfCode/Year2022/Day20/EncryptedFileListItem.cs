namespace AdventOfCode.Year2022.Day20
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EncryptedFileListItem
    {
        private EncryptedFileListItem? adjustedPrevious;
        private EncryptedFileListItem? adjustedNext;

        public long Number { get; set; }

        public long NumberOffset { get; set; }

        public EncryptedFileListItem AdjustedNext
        {
            get => this.adjustedNext ?? throw new InvalidOperationException("AdjustedNext has not been set.");
            set => this.adjustedNext = value;
        }

        public EncryptedFileListItem AdjustedPrevious
        {
            get => this.adjustedPrevious ?? throw new InvalidOperationException("AdjustedPrevious has not been set.");
            set => this.adjustedPrevious = value;
        }

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
                current = current.AdjustedNext;
            }

            return current;
        }

        public EncryptedFileListItem MoveBack(long count)
        {
            EncryptedFileListItem current = this.AdjustedPrevious;

            for (int i = 0; i < count; i++)
            {
                current = current.AdjustedPrevious;
            }

            return current;
        }
    }
}
