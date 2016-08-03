namespace Server.Core
{
    public class Chunk
    {
        public readonly string ChunkInformation;
        public readonly long FileChunkNumber;

        public Chunk(string chunkInformation, long fileChunkNumber, ChunkOriginType chunkOriginType)
        {
            ChunkInformation = chunkInformation;
            FileChunkNumber = fileChunkNumber;
        }
    }
}