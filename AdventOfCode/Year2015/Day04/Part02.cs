namespace AdventOfCode.Year2015.Day04
{
    using System.Security.Cryptography;
    using System.Text;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            int current = 1;
            var hasher = MD5.Create();

            while (true)
            {
                byte[] hashbytes = Encoding.UTF8.GetBytes(input + current);
                byte[] hash = hasher.ComputeHash(hashbytes);

                // To convert this back into a string representation of the hash, we'd concatenate the 2-digit hex
                // representations of each byte in the result. That means that in order for the first five digits to
                // be 0, the first two bytes must be 0 and the third must be less than 0x10.
                if (hash[0] == 0 && hash[1] == 0 && hash[2] == 0)
                {
                    return current.ToString();
                }

                current++;
            }
        }
    }
}
