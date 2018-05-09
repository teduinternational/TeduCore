using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeduCore.Services.Interfaces;

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
            return View(_commonService.GetSlides("brand"));
        }
    }
}