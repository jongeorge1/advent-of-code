namespace AdventOfCode.Year2016.Day05
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            int current = 1;

            var hasher = MD5.Create();

            string passcode = string.Empty;

            while (passcode.Length < 8)
            {
                byte[] currentBytes = Encoding.UTF8.GetBytes(input + current);
                byte[] hash = hasher.ComputeHash(currentBytes);

                // To convert this back into a string representation of the hash, we'd concatenate the 2-digit hex
                // representations of each byte in the result. That means that in order for the first five digits to
                // be 0, the first two bytes must be 0 and the third must be less than 0x10.
                if (hash[0] == 0 && hash[1] == 0 && hash[2] < 0x10)
                {
                    string hashedValue = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
                    passcode += hashedValue[5];
                }

                ++current;
            }

            return passcode;
        }
    }
}
