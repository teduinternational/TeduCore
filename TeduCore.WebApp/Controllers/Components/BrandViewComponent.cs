using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeduCore.Application.Systems.Commons;
using TeduCore.Data.Enums;

namespace TeduCore.WebApp.Controllers.Components
{
    public class BrandViewComponent : ViewComponent
    {
        private ICommonService _commonService;

        public BrandViewComponent(ICommonService commonService)
        {
            _commonService = commonService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View(_commonService.GetSlides(SlideGroup.Branch)));

        }
    }
}