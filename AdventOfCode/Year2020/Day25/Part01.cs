﻿namespace AdventOfCode.Year2020.Day25
{
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            const long subjectNumber = 7;
            long cardPublicKey = long.Parse(input[0]);
            long doorPublicKey = long.Parse(input[1]);

            long cardLoopSize = CalculateLoopSize(subjectNumber, cardPublicKey);
            long encryptionKey = CalculateEncryptionKey(cardLoopSize, doorPublicKey);

            return encryptionKey.ToString();
        }

        private static long CalculateLoopSize(long subjectNumber, long publicKey)
        {
            long loopSize = 0;
            long current = 1;

            while (current != publicKey)
            {
                current = (current * subjectNumber) % 20201227;
                ++loopSize;
            }

            return loopSize;
        }

        private static long CalculateEncryptionKey(long loopSize, long publicKey)
        {
            long current = 1;

            for (int i = 0; i < loopSize; i++)
            {
                current = (current * publicKey) % 20201227;
            }

            return current;
        }
    }
}
