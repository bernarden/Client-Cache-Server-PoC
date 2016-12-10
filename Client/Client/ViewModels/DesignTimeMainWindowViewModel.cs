using System.Collections.ObjectModel;

namespace Client.ViewModels
{
    public class DesignTimeMainWindowViewModel : IMainWindowViewModel
    {
        public ObservableCollection<FileViewModel> Files => new ObservableCollection<FileViewModel>
        {
            new FileViewModel {Name = "ljkNSDVpun:ASbvzdthjfyuikloNSRv", FileStatusType = FileStatusType.Downloaded},
            new FileViewModel {Name = "name3", FileStatusType = FileStatusType.NotDownloaded},
            new FileViewModel {Name = "o;uhseloifuhaslirefj", FileStatusType = FileStatusType.Downloading},
            new FileViewModel {Name = ".kuahskugaseufheeg", FileStatusType = FileStatusType.NotDownloaded},
            new FileViewModel {Name = "kousadhfvpiNSLPIUfpl", FileStatusType = FileStatusType.Downloaded},
            new FileViewModel {Name = ";o;iahewiunw;oef", FileStatusType = FileStatusType.Downloading},
            new FileViewModel {Name = "aw;iouedhvoiauwjefihsdg", FileStatusType = FileStatusType.NotDownloaded},
            new FileViewModel {Name = "liaysdhblviujaweorg", FileStatusType = FileStatusType.NotDownloaded},
            new FileViewModel {Name = "'EWRIOJVPAOSHEVFOI;AJS;FO", FileStatusType = FileStatusType.Downloading},
            new FileViewModel {Name = "kousadhfvp;oiauehr", FileStatusType = FileStatusType.Downloaded},
            new FileViewModel {Name = "liUSJEFIUAJNSELKUIYFRH", FileStatusType = FileStatusType.NotDownloaded}
        };

        public string DownloadOpenFileButtonText => "Download File";

        public FileViewModel SelectedFile { get; set; }
        public bool OperationInProgress { get; set; }

        public void DownloadOpenFile()
        {
        }

        public void RefreshList()
        {
        }

        public void ClearDownloads()
        {
        }
    }
}