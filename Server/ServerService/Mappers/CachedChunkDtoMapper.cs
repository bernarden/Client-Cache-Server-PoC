using Server.Core;
using ServerService.Dtos;

namespace ServerService.Mappers
{
    public static class CachedChunkDtoMapper
    {
        public static Chunk Map(CachedChunkDto cachedChunkDto)
        {
            return new Chunk(cachedChunkDto.ChunkHash, string.Empty, cachedChunkDto.CachedFileChunkLocation);
        }
    }
}