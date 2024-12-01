namespace AdventOfCode.Year2022.Day20
{
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            var file = new EncryptedFile(input);
            file.ApplyDecryptionKey(1);

            var mixer = new Mixer(file);

            mixer.Mix();

            return file.ReadAndSumCoordinates().ToString();
        }
    }
}
