using System.Collections.Generic;
using TeduCore.Application.Content.Slides.Dtos;
using TeduCore.Application.Systems.Settings.Dtos;
using TeduCore.Application.ViewModels;

namespace TeduCore.Application.Systems.Commons
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();

        List<SlideViewModel> GetSlides(string groupAlias);

        SystemConfigViewModel GetSystemConfig(string code);
    }
}