using System.Security.Cryptography;

namespace CalculatorShell.Infrastructure
{
    internal static class HashComputer
    {
        public static async Task<byte[]> ComputeHash(string file, HashAlgorithm algorithm, IProgress<double> progress, CancellationToken cancellationToken)
        {
            byte[]? buffer = new byte[1024 * 4];
            int read = 0;
            long processed = 0;
            using (FileStream? fileStream = File.OpenRead(file))
            {
                long fileSize = fileStream.Length;
                do
                {
                    read = await fileStream.ReadAsync(buffer, 0, buffer.Length);
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    processed += read;

                    if (read == 0)
                    {
                        algorithm.TransformFinalBlock(buffer, 0, read);
                    }
                    else
                    {
                        algorithm.TransformBlock(buffer, 0, read, buffer, 0);
                    }

                    progress.Report((double)processed / fileSize);
                }
                while (read > 0);
            }

            if (algorithm.Hash == null)
                throw new InvalidOperationException();
            else
                return algorithm.Hash;
        }
    }
}
