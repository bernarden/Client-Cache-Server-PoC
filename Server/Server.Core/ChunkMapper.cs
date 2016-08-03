namespace Server.Core
{
    public static class ChunkMapper
    {
        public static Chunk Map(CachedChunkDto cachedChunkDto)
        {
            return new Chunk(cachedChunkDto._chunkInformation, cachedChunkDto._cachedFileChunkLocation, ChunkOriginType.CachedFile);
        }
    }
}