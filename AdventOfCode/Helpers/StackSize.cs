namespace AdventOfCode.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    public static class StackSize
    {
        [DllImport("kernel32.dll")]
        private static extern void GetCurrentThreadStackLimits(out uint lowLimit, out uint highLimit);

        public static uint GetAllocatedStackSize()
        {
            GetCurrentThreadStackLimits(out uint low, out uint high);
            return (high - low) / 1024;
        }
    }
}
