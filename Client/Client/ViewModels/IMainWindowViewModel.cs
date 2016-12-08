using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Client.ViewModels
{
    public interface IMainWindowViewModel
    {
        ObservableCollection<FileViewModel> Files { get; }
        FileViewModel SelectedFile { get; set; }
        string DownloadOpenFileButtonText { get; }
        bool OperationInProgress { get; set; }
        void DownloadOpenFile();
        void RefreshList();
        void ClearDownloads();
    }
}