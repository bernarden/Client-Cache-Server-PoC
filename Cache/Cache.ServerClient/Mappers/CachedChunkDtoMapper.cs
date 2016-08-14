using Cache.ServerClient.ServiceReference;
using Common;

namespace Cache.ServerClient.Mappers
{
    public static class CachedChunkDtoMapper
    {
        public static CachedChunkDto Map(Chunk chunk)
        {
            CachedChunkDto cachedChunkDto = new CachedChunkDto()
            {
                CachedFileChunkLocation = chunk.FileChunkNumber,
                ChunkHash = chunk.ChunkHash
            };

            return cachedChunkDto;
        }
    }
}
