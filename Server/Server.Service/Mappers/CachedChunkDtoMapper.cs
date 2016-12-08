using Common;
using Server.Service.Dtos;

namespace Server.Service.Mappers
{
    public static class CachedChunkDtoMapper
    {
        public static Chunk Map(CachedChunkDto cachedChunkDto)
        {
            return new Chunk(cachedChunkDto.ChunkHash, new byte[0], cachedChunkDto.CachedFileChunkLocation);
        }
    }
}