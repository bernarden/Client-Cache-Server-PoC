using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Client.ViewModels
{
    public class DesignTimeMainWindowViewModel : IMainWindowViewModel
    {
        public ObservableCollection<FileViewModel> Files => new ObservableCollection<FileViewModel>
        {
            new FileViewModel{Name = "ljkNSDVpun:ASOUnv;OZMSCiomSE:IUcm;mcSMNEPUVNPUDvniwegbvzdthjfyuikloNSRv",IsCached = true},
            new FileViewModel {Name = "name3", IsCached = false},
            new FileViewModel {Name = "name3nsdgiuarselignvASDv", IsCached = false},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";klushfBNUKYBCYECYRSGHYFYSBLUVNBABCNSUEBFbYBLUSNEyvbSBCSIRUgn", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = "фжущшмьзщшЬЫВсзщшьЯВСдгтавмним", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";;SEKJnfgpSNDV", IsCached = false},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";;o';u;we;a;v;'h[omnoiMSZDcv", IsCached = false},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = ";kousadhfvpiNSLPIUfpl", IsCached = true},
            new FileViewModel {Name = "name4", IsCached = true}
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