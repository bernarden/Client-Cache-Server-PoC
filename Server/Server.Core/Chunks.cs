using System.Collections.Generic;

namespace Server.Core
{
    public class Chunks
    {
        readonly List<Chunk> _listOfChunks = new List<Chunk>();

        public void AddChunk(string chunkInformation, ChunkOriginType chunkOriginType)
        {
            _listOfChunks.Add(new Chunk(chunkInformation, _listOfChunks.Count, chunkOriginType));
        }

        public List<Chunk> ToList()
        {
            return _listOfChunks;
        }
    }
}