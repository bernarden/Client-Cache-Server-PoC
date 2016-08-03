namespace Server.Core
{
    public class DifferenceChunk
    {
        public readonly string ChunkInformation;
        public readonly long CurentFileChunkNumber;
        public readonly long MidifiedFileChunkNumber;
        public readonly DifferenceChunkType ChunkType;

        public DifferenceChunk(string chunkInformation, long curentFileChunkNumber, long midifiedFileChunkNumber, DifferenceChunkType chunkType)
        {
            ChunkInformation = chunkInformation;
            CurentFileChunkNumber = curentFileChunkNumber;
            MidifiedFileChunkNumber = midifiedFileChunkNumber;
            ChunkType = chunkType;
        }
    }
}