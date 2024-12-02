namespace AdventOfCode.Year2016.Day05
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            int current = 1;

            var hasher = MD5.Create();

            char[] passcode = new char[8];

            int filledPasscodePositions = 0;

            while (filledPasscodePositions < 8)
            {
                byte[] currentBytes = Encoding.UTF8.GetBytes(input[0] + current);
                byte[] hash = hasher.ComputeHash(currentBytes);

                // To convert this back into a string representation of the hash, we'd concatenate the 2-digit hex
                // representations of each byte in the result. That means that in order for the first five digits to
                // be 0, the first two bytes must be 0 and the third must be less than 0x10.
                if (hash[0] == 0 && hash[1] == 0 && hash[2] < 0x10)
                {
                    string hashedValue = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();

                    if (int.TryParse(hashedValue.Substring(5, 1), out int targetPosition) && targetPosition < 8 && passcode[targetPosition] == default(char))
                    {
                        ////Console.WriteLine("Using hash: " + hashedValue);
                        passcode[targetPosition] = hashedValue[6];
                        ++filledPasscodePositions;
                    }
                    ////else
                    ////{
                    ////    Console.WriteLine("Cannot use hash: " + hashedValue);
                    ////}
                }

                ++current;
            }

            return new string(passcode);
        }
    }
}
