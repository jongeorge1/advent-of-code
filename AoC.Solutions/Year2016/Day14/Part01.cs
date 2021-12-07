namespace AoC.Solutions.Year2016.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class Part01 : ISolution
    {
        private readonly MD5 hasher = MD5.Create();

        public string Solve(string input)
        {
            int testIndex = 0;
            var keys = new List<int>();
            var hashes = new List<string>();

            // We need to be able to look ahead, so wwe'll start by populating the first 1001 hashes
            while (keys.Count < 64)
            {
                // Make sure we have enough hashes for the look-ahead
                while (hashes.Count <= testIndex + 1000)
                {
                    hashes.Add(this.GetHash(input, hashes.Count));
                }

                // Now see if the current hash is a key
                if (this.IsKey(hashes, testIndex))
                {
                    keys.Add(testIndex);
                }

                ++testIndex;
            }

            return keys.Last().ToString();
        }

        private bool IsKey(List<string> hashes, int testIndex)
        {
            string hash = hashes[testIndex];

            // Does it contain a triple
            for (int i = 0; i < hash.Length - 2; ++i)
            {
                char matchChar = hash[i];
                if (hash[i + 1] == matchChar && hash[i + 2] == matchChar)
                {
                    string searchString = new (new[] { matchChar, matchChar, matchChar, matchChar, matchChar });
                    return hashes.Skip(testIndex + 1).Take(1000).Any(x => x.IndexOf(searchString) != -1);
                }
            }

            return false;
        }

        private string GetHash(string input, int offset)
        {
            byte[] hashbytes = Encoding.UTF8.GetBytes(input + offset);
            byte[] hash = this.hasher.ComputeHash(hashbytes);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}