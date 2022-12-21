namespace AdventOfCode.Year2022.Day20
{
    public class Mixer
    {
        private readonly EncryptedFile file;

        public Mixer(EncryptedFile file)
        {
            this.file = file;
        }

        public void Mix()
        {
            for (int index = 0; index < this.file.ListItems.Length; ++index)
            {
                var current = this.file.ListItems[index];

                if (current.Number == 0)
                {
                    continue;
                }

                var currentPrevious = current.AdjustedPrevious;
                var currentNext = current.AdjustedNext;

                // Before we do the "move", we need to remove "current" from the list in case there's a scenario
                // where we wrap multiple times - it needs to take account of the fact that we've already taken
                // current out.
                currentPrevious.AdjustedNext = currentNext;
                currentNext.AdjustedPrevious = currentPrevious;

                var targetPrevious = current.Move(current.Number);
                var targetNext = targetPrevious.AdjustedNext;

                // Now insert it in the target location
                targetPrevious.AdjustedNext = current;
                targetNext.AdjustedPrevious = current;
                current.AdjustedPrevious = targetPrevious;
                current.AdjustedNext = targetNext;
            }
        }
    }
}
