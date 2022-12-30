namespace AdventOfCode.Year2022.Day25
{
    using System;
    using System.Text;

    public static class SnafuConverter
    {
        public static long ToLong(ReadOnlySpan<char> snafu)
        {
            long result = 0;

            for (int pow = 0; pow < snafu.Length; ++pow)
            {
                int pos = snafu.Length - pow - 1;

                result += snafu[pos] switch
                {
                    '2' => 2 * (long)Math.Pow(5, pow),
                    '1' => (long)Math.Pow(5, pow),
                    '0' => 0,
                    '-' => -(long)Math.Pow(5, pow),
                    '=' => -2 * (long)Math.Pow(5, pow),
                    _ => throw new Exception(),
                };
            }

            return result;
        }

        public static ReadOnlySpan<char> ToSnafu(long number)
        {
            StringBuilder snafuNumber = new();

            while (number > 0)
            {
                (number, long remainder) = Math.DivRem(number, 5);
                snafuNumber.Insert(0, GetSnafuCharacter(remainder));

                if (remainder > 2)
                {
                    number += 1;
                }
            }

            return snafuNumber.ToString().AsSpan();
        }

        private static char GetSnafuCharacter(long input)
        {
            return input switch
            {
                0 => '0',
                1 => '1',
                2 => '2',
                3 => '=',
                4 => '-',
                _ => throw new Exception(),
            };
        }
    }
}
