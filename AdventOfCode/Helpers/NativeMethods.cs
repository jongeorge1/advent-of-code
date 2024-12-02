namespace AdventOfCode.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        public static uint GetAllocatedStackSize()
        {
            GetCurrentThreadStackLimits(out uint low, out uint high);
            return (high - low) / 1024;
        }

        [DllImport("kernel32.dll")]
        private static extern void GetCurrentThreadStackLimits(out uint lowLimit, out uint highLimit);
    }
}
