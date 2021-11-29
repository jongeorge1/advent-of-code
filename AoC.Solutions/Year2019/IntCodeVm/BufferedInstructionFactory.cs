namespace AoC.Solutions.Year2019.IntCodeVm
{
    using System.Collections.Concurrent;

    public static class BufferedInstructionFactory
    {
        private static readonly ConcurrentDictionary<int, BufferedInstruction> Cache = new ConcurrentDictionary<int, BufferedInstruction>();

        public static BufferedInstruction GetBufferedInstruction(int instruction)
        {
            return Cache.GetOrAdd(instruction, x => new BufferedInstruction(x));
        }
    }
}
