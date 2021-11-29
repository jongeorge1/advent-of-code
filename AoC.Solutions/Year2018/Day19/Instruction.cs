#nullable disable
namespace AoC.Solutions.Year2018.Day19
{
    using System.Diagnostics;

    [DebuggerDisplay("{OpName} {A} {B} {C}")]
    public class Instruction
    {
        public string OpName { get; set; }

        public int A { get; set; }

        public int B { get; set; }

        public int C { get; set; }
    }
}
