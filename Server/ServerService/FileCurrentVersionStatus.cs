using System.Runtime.Serialization;

namespace Server.Service
{
    [DataContract]
    public enum FileCurrentVersionStatus
    {
        [EnumMember]
        UpToDate,

        [EnumMember]
        Modified,

        [EnumMember]
        Removed
    }
}