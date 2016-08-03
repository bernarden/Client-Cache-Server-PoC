using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Core
{
    public static class ChunkDifferentiator
    {
        public static IEnumerable<DifferenceChunk> GetDifferenceChunks(List<Chunk> chunksFromCachedFile, List<Chunk> chunksFromCurrentFile)
        {
            int j = 0;
            Console.WriteLine("\n--------MATCHED CHUNKS--------\n");
            int matchCount = 0;
            foreach (Chunk chunk in chunksFromCachedFile)
            {
                if (chunksFromCurrentFile.Any(x => x.ChunkInformation.Equals(chunk.ChunkInformation)))
                {

                    Console.WriteLine("{0:D4}<-->{1:D4}  (size:{2:D8} bytes)", j, chunksFromCurrentFile.First(x => x.ChunkInformation.Equals(chunk.ChunkInformation)).FileChunkNumber, chunk.ChunkInformation.Length);
                    matchCount++;
                }
                else
                    Console.WriteLine("{0:D4}    *     (size:{1:D8} bytes)", j, chunk.ChunkInformation.Length);
                j++;
            }
            Console.WriteLine("\n------------------------------\n");
            Console.WriteLine("Matched blockes {0} (out of {1})", matchCount, chunksFromCachedFile.Count);

            List<DifferenceChunk> matchedUpChunksTogether = MatchUpChunksTogether(chunksFromCachedFile, chunksFromCurrentFile);

            return PickDifferenceChunks(matchedUpChunksTogether);
        }

        private static IEnumerable<DifferenceChunk> PickDifferenceChunks(List<DifferenceChunk> matchedUpChunksTogether)
        {
            var differenceChunksToProcess = new List<DifferenceChunk>();
            var chunksToReturnToCacheMachine = new List<DifferenceChunk>();

            foreach (DifferenceChunk chunk in matchedUpChunksTogether)
            {
                if (chunk.ChunkType != DifferenceChunkType.Undetermined)
                {
                    differenceChunksToProcess.Add(chunk);
                }
                else
                {
                    // First not modified chunk;
                    if (!differenceChunksToProcess.Any())
                    {
                        continue;
                    }
                    // chunks to preocess
                    chunksToReturnToCacheMachine = chunksToReturnToCacheMachine.Concat(ProcessDifferenceChunks(chunksToReturnToCacheMachine)).ToList();
                }
            }

            return chunksToReturnToCacheMachine;
        }

        private static IEnumerable<DifferenceChunk> ProcessDifferenceChunks(List<DifferenceChunk> chunksToReturnToCacheMachine)
        {
            List<DifferenceChunk> cachedDifferenceChunks = chunksToReturnToCacheMachine.Where(x => x.MidifiedFileChunkNumber != long.MinValue).Select(x => x).ToList();
            List<DifferenceChunk> currentDifferenceChunks = chunksToReturnToCacheMachine.Where(x => x.CurentFileChunkNumber != long.MinValue).Select(x => x).ToList();



        }

        /// <summary>
        /// Matches up chunks together.
        /// </summary>
        private static List<DifferenceChunk> MatchUpChunksTogether(List<Chunk> chunksFromOriginalFile, List<Chunk> chunksFromModifiedFile)
        {
            List<DifferenceChunk> differenceChunks = new List<DifferenceChunk>();

            for (int i = chunksFromOriginalFile.Count - 1; i >= 0; i--)
            {
                Chunk originalChunks = chunksFromOriginalFile.ElementAt(i);
                Chunk matchingModifiedChunk = chunksFromModifiedFile.SingleOrDefault(x => x.ChunkInformation.Equals(originalChunks.ChunkInformation));
                if (matchingModifiedChunk != null)
                {
                    // Found matching chunk from modified file.
                    chunksFromModifiedFile.Remove(matchingModifiedChunk);
                    differenceChunks.Add(new DifferenceChunk(originalChunks.ChunkInformation, originalChunks.FileChunkNumber, matchingModifiedChunk.FileChunkNumber, DifferenceChunkType.NotModified));
                }
                else
                {
                    // Did NOT find matching chunk from modified file. This chunk was either modified or deleted.
                    differenceChunks.Add(new DifferenceChunk(originalChunks.ChunkInformation, originalChunks.FileChunkNumber, long.MinValue, DifferenceChunkType.Undetermined));
                }
                chunksFromOriginalFile.RemoveAt(i);
            }

            foreach (Chunk modifiedChunks in chunksFromModifiedFile)
            {
                // NewFile Chunks that havent been mapped to any original chunks
                differenceChunks.Add(new DifferenceChunk(modifiedChunks.ChunkInformation, long.MinValue, modifiedChunks.FileChunkNumber, DifferenceChunkType.Undetermined));
            }
            return differenceChunks;
        }
    }
}
