using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// Implementation of Rabin-Karp algorithm
    /// </summary>
    public static class RabinKarpAlgorithm
    {
        private const ulong Boundary = 0x1FFF;
        private const ulong Q = 541; //8101;//32416190071;
        private const ulong D = 256;
        private const int WindowSize = 48;

        /// <summary>
        /// Slices the given file content.
        /// </summary>
        public static List<Chunk> Slice(string fileContent)
        {
            List<Chunk> chunks = new List<Chunk>();
            ulong pow = 1;
            for (int k = 1; k < WindowSize; k++)
                pow = (pow * D) % Q;
            ulong sig = 0;
            int lastIndex = 0;
            for (int i = 0; i < WindowSize; i++)
                sig = (sig * D + fileContent[i]) % Q;
            for (int j = 1; j <= fileContent.Length - WindowSize; j++)
            {
                sig = (sig + Q - pow * fileContent[j - 1] % Q) % Q;
                sig = (sig * D + fileContent[j + WindowSize - 1]) % Q;
                if ((sig & Boundary) == 0)
                {
                    if (j + 1 - lastIndex >= 2048)
                    {
                        chunks.Add(new Chunk(fileContent.Substring(lastIndex, j + 1 - lastIndex), chunks.Count));
                        lastIndex = j + 1;
                    }
                }
                else if (j + 1 - lastIndex >= 65536)
                {
                    chunks.Add(new Chunk(fileContent.Substring(lastIndex, j + 1 - lastIndex), chunks.Count));
                    lastIndex = j + 1;
                }
            }
            if (lastIndex < fileContent.Length - 1)
                chunks.Add(new Chunk(fileContent.Substring(lastIndex), chunks.Count));
            return chunks;
        }
    }
}
