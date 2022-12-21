namespace AdventOfCode.Year2022.Day20
{
    using System;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var file = new EncryptedFile(input);
            file.ApplyDecryptionKey(811589153);

            var mixer = new Mixer(file);
            mixer.Mix();
            mixer.Mix();
            mixer.Mix();
            mixer.Mix();
            mixer.Mix();
            mixer.Mix();
            mixer.Mix();
            mixer.Mix();
            mixer.Mix();
            mixer.Mix();

            return file.ReadAndSumCoordinates().ToString();
        }
    }
}
