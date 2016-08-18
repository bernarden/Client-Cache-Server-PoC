using System;
using System.Collections.Generic;

namespace Cache.App
{
    public class MainWindowDesignTimeViewModel : IMainWindowViewModel
    {
        public List<FileViewModel> Files => new List<FileViewModel>
        {
            new FileViewModel {Name = "ljkNSDVpun:ASOUnv;OZMSCiomSE:IUcm;mcSMNEPUVNPUDvniwegbvzdthjfyuikloNSRv", IsCached = true},
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

        public List<LogMessageViewModel> LogMessages => new List<LogMessageViewModel>()
        {
            new LogMessageViewModel { LogTime = DateTime.Now.Subtract(TimeSpan.FromDays(-10)), Message = "Message1ytiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiouiSDVNliusneLIUnliuVNIUS;UOSDHvkjNSBYTBTCBPLNENBHefiub2USDv;uN:SIUFnIAEBfuib21i3b4i23b5j345n7ujn;ladbnvl;hBNSEn;iiiiiiiiiiiiiii"},
            new LogMessageViewModel { LogTime = DateTime.Now.Subtract(TimeSpan.FromDays(-7)), Message = "Message1 LEIUFGNPAOISMVPOIM'oui'@as;'r;;gp'i@@u'hs'pi@;@uv'hn"},
            new LogMessageViewModel { LogTime = DateTime.Now.Subtract(TimeSpan.FromDays(-5)), Message = "Message1"},
            new LogMessageViewModel { LogTime = DateTime.Now.Subtract(TimeSpan.FromHours(1)), Message = ";OISrviplunSILUCNSKYBCKsybdCKYbskdYVBl;iaeurvpliunSPUVpsneivUBNsuebIbsecBsuebIYsbnpivpiusbnVUnsivcunsoyvHB"},
            new LogMessageViewModel { LogTime = DateTime.Now.Subtract(TimeSpan.FromHours(2)), Message = "Message1"},
        };

        public void ClearCache() { }
    }
}