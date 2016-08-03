namespace Server.Core
{
    public class DifferenceChunk
    {
        public readonly string ChunkInformation;
        public readonly long CurentFileChunkNumber;
        public readonly long CachedFileChunkNumber;

        public DifferenceChunk(string chunkInformation, long curentFileChunkNumber, long cachedFileChunkNumber)
        {
            ChunkInformation = chunkInformation;
            CurentFileChunkNumber = curentFileChunkNumber;
            CachedFileChunkNumber = cachedFileChunkNumber;
        }
    }
}