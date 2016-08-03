using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Server.Core;

namespace Server.Test
{
    [TestFixture]
    class GetDifferenceChunksTest
    {
        //  ↑ ↓

        [Test]
        //  original: no chunks
        //             
        //  modified: no chunks
        public void TestCase1()
        {
            List<Chunk> chunksFromModifiedFile = new List<Chunk>();
            List<Chunk> chunksFromOriginalFile = new List<Chunk>();

            IEnumerable<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetDifferenceChunks(chunksFromOriginalFile, chunksFromModifiedFile);

            differenceChunks.Count().Should().Be(0);
        }

        [Test]
        //  original: [0]
        //             ↓
        //  modified: [0]
        public void TestCase2()
        {
            List<Chunk> chunksFromOriginalFile = new List<Chunk> { new Chunk("chunk", 0, ChunkOriginType.CachedFile) };
            List<Chunk> chunksFromModifiedFile = new List<Chunk> { new Chunk("chunk", 0, ChunkOriginType.CurrentFile) };

            IEnumerable<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetDifferenceChunks(chunksFromOriginalFile, chunksFromModifiedFile);

            differenceChunks.Count().Should().Be(0);
        }

        [Test]
        //  original: [0]
        //             ↓
        //  modified: [0][1]
        public void TestCase3()
        {
            List<Chunk> chunksFromOriginalFile = new List<Chunk> { new Chunk("chunk1", 0, ChunkOriginType.CachedFile) };
            List<Chunk> chunksFromModifiedFile = new List<Chunk> { new Chunk("chunk1", 0, ChunkOriginType.CurrentFile), new Chunk("chunk2", 1, ChunkOriginType.CurrentFile) };

            List<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetDifferenceChunks(chunksFromOriginalFile, chunksFromModifiedFile).ToList();

            differenceChunks.Count().Should().Be(1);
            differenceChunks.Single().ChunkInformation.Should().Be(chunksFromModifiedFile.Last().ChunkInformation);
            differenceChunks.Single().CurentFileChunkNumber.Should().Be(1);
            differenceChunks.Single().ChunkType.Should().Be(DifferenceChunkType.Added);
        }

    }
}
