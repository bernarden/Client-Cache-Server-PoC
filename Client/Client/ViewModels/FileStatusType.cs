using System.ComponentModel;

namespace Client.ViewModels
{
    public enum FileStatusType
    {
        [Description("")]
        NotDownloaded,

        [Description("Downloading...")]
        Downloading,

        [Description("Downloaded")]
        Downloaded

    }
}