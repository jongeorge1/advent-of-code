namespace AoC.Solutions.Year2016.Day04
{
    using System.Diagnostics;
    using System.Linq;

    [DebuggerDisplay("{Name}-{SectorId}[{Checksum}]")]
    public class Room
    {
        private const int NameOffset = 'a';

        public Room(string input)
        {
            int indexOfChecksum = input.IndexOf('[');
            int indexOfSectorId = input.LastIndexOf('-');

            this.Name = input[0..indexOfSectorId];
            this.SectorId = int.Parse(input[(indexOfSectorId + 1) ..indexOfChecksum]);
            this.Checksum = input[(indexOfChecksum + 1) ..^1];

            string expectedChecksum = string.Concat(this.Name.Where(x => x != '-').GroupBy(x => x).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Select(x => x.Key).Take(5));
            this.IsValid = this.Checksum == expectedChecksum;
        }

        public string Name { get; }

        public int SectorId { get; }

        public string Checksum { get; }

        public bool IsValid { get; }

        public string DecryptedName()
        {
            char[] decryptedName = new char[this.Name.Length];

            for (int i = 0; i < this.Name.Length; ++i)
            {
                if (this.Name[i] == '-')
                {
                    decryptedName[i] = '-';
                }
                else
                {
                    decryptedName[i] = (char)(((this.Name[i] - NameOffset + this.SectorId) % 26) + NameOffset);
                }
            }

            return new string(decryptedName);
        }
    }
}
