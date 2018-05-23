using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCore.Application.Systems.Functions;
using TeduCore.Application.Systems.Functions.Dtos;
using TeduCore.WebApi.Authorization;

namespace TeduCore.WebApi.Controllers
{
    public class FunctionController : ApiController
    {
        private readonly IFunctionService _functionService;

        public FunctionController(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        [Authorize(PermissionItem.Function, PermissionAction.Read)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<FunctionViewModel> model;
            if (User.IsInRole("Admin"))
            {
                model = _functionService.GetAll();
            }
            else
            {
                model = await _functionService.GetAllWithPermission(User.Identity.Name);
            }
            var parents = model.Where(x => x.ParentId == null);
            foreach (var parent in parents)
            {
                parent.ChildFunctions = model.Where(x => x.ParentId == parent.Id).ToList();
            }
            return new OkObjectResult(parents);
        }
    }
}