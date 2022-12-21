namespace AdventOfCode.Year2022.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AdventOfCode;
    using AdventOfCode.Helpers;

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EncryptedFileListItem
    {
        public int Number { get; init; }

        public EncryptedFileListItem AdjustedNext { get; set; }

        public EncryptedFileListItem AdjustedPrevious { get; set; }

        private string DebuggerDisplay
        {
            get => $"{AdjustedPrevious.Number}, [{Number}], {AdjustedNext.Number}";
        }

        public EncryptedFileListItem Move(int count)
        {
            return count < 0
                ? this.MoveBack(count * -1)
                : this.MoveForward(count);
        }

        public EncryptedFileListItem MoveForward(int count)
        {
            EncryptedFileListItem current = this;

            for (int i = 0; i < count; i++)
            {
                current = current.AdjustedNext;
            }

            return current;
        }

        public EncryptedFileListItem MoveBack(int count)
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
