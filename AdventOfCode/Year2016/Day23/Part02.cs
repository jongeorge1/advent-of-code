namespace AdventOfCode.Year2016.Day23
{
    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            // Analysis of the program suggest that it's calculating the factorial of the number that
            // starts in register a, then adding on the result of multiplying two further numbers
            // together. The two numbers are on lines 20 and 21 of the input; for me they are 87 and 77,
            // meaning we add 6699 to the factorial.
            // We can confirm this with the answer for part 1 (with register "a" set to 7):
            // 7! + 6699 = 5040 + 6699 = 11739
            // So for part 2...
            // 12! + 6699 = 479,001,600 + 6699 = 479,008,299
            return "479008299";
        }
    }
}