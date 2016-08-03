namespace Server.Core
{
    public class CachedChunkDto
    {
        public long _cachedFileChunkLocation;
        public string _chunkInformation;
        public CachedChunkDto(long cachedFileChunkLocation, string chunkInformation)
        {
            _cachedFileChunkLocation = cachedFileChunkLocation;
            _chunkInformation = chunkInformation;
        }
    }
}