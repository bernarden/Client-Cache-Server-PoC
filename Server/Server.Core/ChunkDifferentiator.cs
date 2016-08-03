using System.Collections.Generic;
using System.Linq;

namespace Server.Core
{
    public static class ChunkDifferentiator
    {
        public static IEnumerable<DifferenceChunk> GetUpdatedChunks(List<Chunk> chunksFromCachedFile, List<Chunk> chunksFromCurrentFile)
        {
            List<DifferenceChunk> differenceChunks = new List<DifferenceChunk>(chunksFromCurrentFile.Count);

            foreach (Chunk currentChunk in chunksFromCurrentFile)
            {
                Chunk cachedChunk = chunksFromCachedFile.FirstOrDefault(x => x.ChunkHash.Equals(currentChunk.ChunkHash));
                var differenceChunk = cachedChunk != null ?
                    new DifferenceChunk(string.Empty, currentChunk.FileChunkNumber, cachedChunk.FileChunkNumber) :
                    new DifferenceChunk(currentChunk.ChunkInformation, currentChunk.FileChunkNumber, -1);
                differenceChunks.Add(differenceChunk);
            }

            return differenceChunks;
        }
    }
}
