using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeduCore.WebAdmin.Controllers.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        public SideBarViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View());

        }
    }
}
