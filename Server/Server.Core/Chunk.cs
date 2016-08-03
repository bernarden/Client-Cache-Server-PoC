using Server.Common;

namespace Server.Core
{
    public class Chunk
    {
        public readonly string ChunkInformation;
        public readonly string ChunkHash;
        public readonly long FileChunkNumber;

        public Chunk(string chunkInformation, long fileChunkNumber)
        {
            ChunkInformation = chunkInformation;
            FileChunkNumber = fileChunkNumber;
            ChunkHash = chunkInformation.CalculateSha256Hash();
        }

        public Chunk(string chunkHash, string chunkInformation, long cachedFileChunkLocation)
        {
            ChunkHash = chunkHash;
            ChunkInformation = chunkInformation;
            FileChunkNumber = cachedFileChunkLocation;
        }
    }
}