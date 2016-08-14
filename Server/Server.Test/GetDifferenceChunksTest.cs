using System.Collections.Generic;
using System.Linq;
using Common;
using FluentAssertions;
using NUnit.Framework;
using Server.Core;

namespace Server.Test
{
    [TestFixture]
    class GetDifferenceChunksTest
    {
        [Test]
        //  cache: no chunks
        //             
        //  current: no chunks
        public void TestCase1()
        {
            var chunksFromCurrentFile = new List<Chunk>();
            var chunksFromCachedFile = new List<Chunk>();

            IEnumerable<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetUpdatedChunks(chunksFromCachedFile, chunksFromCurrentFile);

            differenceChunks.Count().Should().Be(0);
        }

        [Test]
        //  cache:    [0]
        //             ↓
        //  current:  [0]
        public void TestCase2()
        {
            var chunksFromCurrentFile = new List<Chunk> { new Chunk("chunk", 0) };
            var chunksFromCachedFile = new List<Chunk> { new Chunk("chunk", 0) };

            IEnumerable<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetUpdatedChunks(chunksFromCachedFile, chunksFromCurrentFile);

            DifferenceChunk chunk = differenceChunks.Single();
            chunk.CachedFileChunkNumber.Should().Be(0);
            chunk.CurentFileChunkNumber.Should().Be(0);
            chunk.ChunkInformation.Should().BeEmpty();
        }

        [Test]
        //  cache:    [0]
        //             ↓
        //  current : [0][1]
        public void TestCase3()
        {
            var chunksFromCachedFile = new List<Chunk> { new Chunk("chunk1", 0) };
            var chunksFromCurrentFile = new List<Chunk> { new Chunk("chunk1", 0), new Chunk("chunk2", 1) };

            List<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetUpdatedChunks(chunksFromCachedFile, chunksFromCurrentFile).ToList();

            differenceChunks.Count.Should().Be(2);
            differenceChunks.ElementAt(0).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(0).CurentFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(0).CachedFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(1).ChunkInformation.Should().NotBeEmpty();
            differenceChunks.ElementAt(1).CurentFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(1).CachedFileChunkNumber.Should().BeNegative();
        }


        [Test]
        //  cache:    [0][1]
        //             ↓
        //  current : [0][1]
        public void TestCase4()
        {
            var chunksFromCachedFile = new List<Chunk> { new Chunk("chunk1", 0), new Chunk("chunk3", 1) };
            var chunksFromCurrentFile = new List<Chunk> { new Chunk("chunk1", 0), new Chunk("chunk2", 1) };

            List<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetUpdatedChunks(chunksFromCachedFile, chunksFromCurrentFile).ToList();

            differenceChunks.Count.Should().Be(2);
            differenceChunks.ElementAt(0).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(0).CurentFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(0).CachedFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(1).ChunkInformation.Should().NotBeEmpty();
            differenceChunks.ElementAt(1).CurentFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(1).CachedFileChunkNumber.Should().BeNegative();
        }

        [Test]
        //  cache:    [0][1]
        //                ↓
        //  current : [0][1]
        public void TestCase5()
        {
            var chunksFromCachedFile = new List<Chunk> { new Chunk("chunk1", 0), new Chunk("chunk3", 1) };
            var chunksFromCurrentFile = new List<Chunk> { new Chunk("chunk2", 0), new Chunk("chunk3", 1) };

            List<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetUpdatedChunks(chunksFromCachedFile, chunksFromCurrentFile).ToList();

            differenceChunks.Count.Should().Be(2);
            differenceChunks.ElementAt(0).ChunkInformation.Should().NotBeEmpty();
            differenceChunks.ElementAt(0).CurentFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(0).CachedFileChunkNumber.Should().BeNegative();
            differenceChunks.ElementAt(1).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(1).CurentFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(1).CachedFileChunkNumber.Should().Be(1);
        }

        [Test]
        //  cache:    [0][1][2]
        //                ↓
        //  current : [0][1]
        public void TestCase6()
        {
            var chunksFromCachedFile = new List<Chunk> { new Chunk("chunk1", 0), new Chunk("chunk3", 1), new Chunk("chunk4", 2) };
            var chunksFromCurrentFile = new List<Chunk> { new Chunk("chunk2", 0), new Chunk("chunk3", 1) };

            List<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetUpdatedChunks(chunksFromCachedFile, chunksFromCurrentFile).ToList();

            differenceChunks.Count.Should().Be(2);
            differenceChunks.ElementAt(0).ChunkInformation.Should().NotBeEmpty();
            differenceChunks.ElementAt(0).CurentFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(0).CachedFileChunkNumber.Should().BeNegative();
            differenceChunks.ElementAt(1).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(1).CurentFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(1).CachedFileChunkNumber.Should().Be(1);
        }

        [Test]
        //  cache:    [0][1][2]
        //                ↓  ↓
        //  current : [0][1][2]
        public void TestCase7()
        {
            var chunksFromCachedFile = new List<Chunk> { new Chunk("chunk1", 0), new Chunk("chunk3", 1), new Chunk("chunk4", 2) };
            var chunksFromCurrentFile = new List<Chunk> { new Chunk("chunk2", 0), new Chunk("chunk3", 1), new Chunk("chunk4", 2) };

            List<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetUpdatedChunks(chunksFromCachedFile, chunksFromCurrentFile).ToList();

            differenceChunks.Count.Should().Be(3);
            differenceChunks.ElementAt(0).ChunkInformation.Should().NotBeEmpty();
            differenceChunks.ElementAt(0).CurentFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(0).CachedFileChunkNumber.Should().BeNegative();
            differenceChunks.ElementAt(1).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(1).CurentFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(1).CachedFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(2).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(2).CurentFileChunkNumber.Should().Be(2);
            differenceChunks.ElementAt(2).CachedFileChunkNumber.Should().Be(2);
        }

        [Test]
        //  cache:    [0][1][2]
        //             ↓  ↓  ↓
        //  current : [0][1][2]
        public void TestCase8()
        {
            var chunksFromCachedFile = new List<Chunk> { new Chunk("chunk1", 0), new Chunk("chunk3", 1), new Chunk("chunk4", 2) };
            var chunksFromCurrentFile = new List<Chunk> { new Chunk("chunk1", 0), new Chunk("chunk3", 1), new Chunk("chunk4", 2) };

            List<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetUpdatedChunks(chunksFromCachedFile, chunksFromCurrentFile).ToList();

            differenceChunks.Count.Should().Be(3);
            differenceChunks.ElementAt(0).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(0).CurentFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(0).CachedFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(1).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(1).CurentFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(1).CachedFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(2).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(2).CurentFileChunkNumber.Should().Be(2);
            differenceChunks.ElementAt(2).CachedFileChunkNumber.Should().Be(2);
        }

        [Test]
        //  cache:       [0][1][2]
        //                ↓  ↓
        //  current : [0][1][2]
        public void TestCase9()
        {
            var chunksFromCachedFile = new List<Chunk> { new Chunk("chunk1", 0), new Chunk("chunk2", 1), new Chunk("chunk3", 2) };
            var chunksFromCurrentFile = new List<Chunk> { new Chunk("chunk0", 0), new Chunk("chunk1", 1), new Chunk("chunk2", 2) };

            List<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetUpdatedChunks(chunksFromCachedFile, chunksFromCurrentFile).ToList();

            differenceChunks.Count.Should().Be(3);
            differenceChunks.ElementAt(0).ChunkInformation.Should().NotBeEmpty();
            differenceChunks.ElementAt(0).CurentFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(0).CachedFileChunkNumber.Should().BeNegative();
            differenceChunks.ElementAt(1).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(1).CurentFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(1).CachedFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(2).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(2).CurentFileChunkNumber.Should().Be(2);
            differenceChunks.ElementAt(2).CachedFileChunkNumber.Should().Be(1);
        }

        [Test]
        //  cache:    [0][1][2][3]
        //                ↓     ↓
        //  current :    [0][1][2]
        public void TestCase10()
        {
            var chunksFromCachedFile = new List<Chunk> { new Chunk("chunk", 0), new Chunk("chunk0", 1), new Chunk("chunk", 2), new Chunk("chunk2", 3), };
            var chunksFromCurrentFile = new List<Chunk> { new Chunk("chunk0", 0), new Chunk("chunk1", 1), new Chunk("chunk2", 2) };

            List<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetUpdatedChunks(chunksFromCachedFile, chunksFromCurrentFile).ToList();

            differenceChunks.Count.Should().Be(3);

            differenceChunks.ElementAt(0).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(0).CurentFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(0).CachedFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(1).ChunkInformation.Should().NotBeEmpty();
            differenceChunks.ElementAt(1).CurentFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(1).CachedFileChunkNumber.Should().BeNegative();
            differenceChunks.ElementAt(2).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(2).CurentFileChunkNumber.Should().Be(2);
            differenceChunks.ElementAt(2).CachedFileChunkNumber.Should().Be(3);
        }

        [Test]
        //  cache:        [ 0 ]
        //                 ↓  ↓
        //  current :     [0][1]
        public void TestCase11()
        {
            var chunksFromCachedFile = new List<Chunk> { new Chunk("chunk", 0) };
            var chunksFromCurrentFile = new List<Chunk> { new Chunk("chunk", 0), new Chunk("chunk", 1) };

            List<DifferenceChunk> differenceChunks = ChunkDifferentiator.GetUpdatedChunks(chunksFromCachedFile, chunksFromCurrentFile).ToList();

            differenceChunks.Count.Should().Be(2);
            differenceChunks.ElementAt(0).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(0).CurentFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(0).CachedFileChunkNumber.Should().Be(0);
            differenceChunks.ElementAt(1).ChunkInformation.Should().BeEmpty();
            differenceChunks.ElementAt(1).CurentFileChunkNumber.Should().Be(1);
            differenceChunks.ElementAt(1).CachedFileChunkNumber.Should().Be(0);
        }
    }
}
