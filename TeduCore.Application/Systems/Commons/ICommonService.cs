using System.Collections.Generic;
using TeduCore.Application.Content.Slides.Dtos;
using TeduCore.Application.Systems.Settings.Dtos;
using TeduCore.Application.ViewModels;
using TeduCore.Data.Enums;

namespace TeduCore.Application.Systems.Commons
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();

        SystemConfigViewModel GetSystemConfig(string code);
        List<SlideViewModel> GetSlides(SlideGroup groupAlias);
    }
}