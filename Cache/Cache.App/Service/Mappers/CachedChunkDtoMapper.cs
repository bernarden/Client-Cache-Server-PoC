using Cache.WPF.ServerReference;
using Common;

namespace Cache.WPF.Service.Mappers
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
