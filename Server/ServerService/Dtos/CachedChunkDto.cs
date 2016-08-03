namespace ServerService.Dtos
{
    public class CachedChunkDto
    {
        public long CachedFileChunkLocation { get; set; }
        public string ChunkHash { get; set; }

    }
}