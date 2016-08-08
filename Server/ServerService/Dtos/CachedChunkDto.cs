using System.Runtime.Serialization;

namespace Server.Service.Dtos
{
    [DataContract]
    public class CachedChunkDto
    {
        [DataMember]
        public long CachedFileChunkLocation { get; set; }

        [DataMember]
        public string ChunkHash { get; set; }
    }
}