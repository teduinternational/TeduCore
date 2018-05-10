using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Data.Entities;
using TeduCore.Services.ViewModels;
using TeduCore.Services.ViewModels.System;

namespace TeduCore.Services.Interfaces
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();
        List<SlideViewModel> GetSlides(string groupAlias);
        SystemConfigViewModel GetSystemConfig(string code);
    }
}
