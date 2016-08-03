namespace Server.Core
{
    public static class RabinKarpAlgorithm
    {
        public static Chunks Slice(string s, ChunkOriginType chunkOriginType)
        {
            Chunks chunks = new Chunks();
            ulong boundary = 0x1FFF;

            ulong Q = 541; //8101;//32416190071;
            ulong D = 256;
            ulong pow = 1;
            int windowSize = 48;
            for (int k = 1; k < windowSize; k++)
                pow = (pow * D) % Q;
            ulong sig = 0;
            int lastIndex = 0;
            for (int i = 0; i < windowSize; i++)
                sig = (sig * D + (ulong)s[i]) % Q;
            for (int j = 1; j <= s.Length - windowSize; j++)
            {
                sig = (sig + Q - pow * (ulong)s[j - 1] % Q) % Q;
                sig = (sig * D + (ulong)s[j + windowSize - 1]) % Q;
                if ((sig & boundary) == 0)
                {
                    if (j + 1 - lastIndex >= 2048)
                    {
                        chunks.AddChunk(s.Substring(lastIndex, j + 1 - lastIndex), chunkOriginType);
                        lastIndex = j + 1;
                    }
                }
                else if (j + 1 - lastIndex >= 65536)
                {
                    chunks.AddChunk(s.Substring(lastIndex, j + 1 - lastIndex), chunkOriginType);
                    lastIndex = j + 1;
                }
            }
            if (lastIndex < s.Length - 1)
                chunks.AddChunk(s.Substring(lastIndex), chunkOriginType);
            return chunks;
        }
    }

    public enum ChunkOriginType
    {
        CachedFile,
        CurrentFile
    }
}