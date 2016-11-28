using System.Collections.Generic;

namespace Client.ViewModels
{
    public interface IMainWindowViewModel
    {
        List<FileViewModel> Files { get; }
        FileViewModel SelectedFile { get; set; }
        bool OperationInProgress { get; set; }
        string DownloadOpenFileButtonText { get; }
        void DownloadOpenFile();
        void RefreshList();
        void ClearDownloads();
    }
}