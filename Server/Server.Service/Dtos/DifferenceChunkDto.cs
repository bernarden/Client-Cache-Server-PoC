using System.Runtime.Serialization;

namespace Server.Service.Dtos
{
    [DataContract]
    public class DifferenceChunkDto
    {
        [DataMember]
        public byte[] ChunkInformation { get; set; }

        [DataMember]
        public long CurentFileChunkNumber { get; set; }

        [DataMember]
        public long CachedFileChunkNumber { get; set; }
    }
}