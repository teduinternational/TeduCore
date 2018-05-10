using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduCore.Application.Systems.Functions;
using TeduCore.Application.Systems.Functions.Dtos;
using TeduCore.Utilities.Constants;

namespace TeduCore.WebApp.Areas.Admin.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IFunctionService _functionService;

        public SideBarViewComponent(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == CommonConstants.UserClaim.Roles);
            List<FunctionViewModel> functions;
            if (roles != null && roles.Value.Split(";").Contains(CommonConstants.AppRole.Admin))
                functions = await _functionService.GetAll(string.Empty);
            else
                functions = await _functionService.GetAllWithPermission(User.Identity.Name);
            return View(functions);
        }
    }
}