namespace Common
{
    /// <summary>
    /// Represents a chunk of a file.
    /// </summary>
    public class Chunk
    {
        public readonly string ChunkInformation;
        public readonly string ChunkHash;
        public readonly long FileChunkNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk"/> class.
        /// </summary>
        public Chunk(string chunkInformation, long fileChunkNumber)
        {
            ChunkInformation = chunkInformation;
            FileChunkNumber = fileChunkNumber;
            ChunkHash = chunkInformation.CalculateSha256Hash();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk"/> class.
        /// </summary>
        public Chunk(string chunkHash, string chunkInformation, long cachedFileChunkLocation)
        {
            ChunkHash = chunkHash;
            ChunkInformation = chunkInformation;
            FileChunkNumber = cachedFileChunkLocation;
        }
    }
}
