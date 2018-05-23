using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCore.Application.Systems.Functions;

namespace TeduCore.WebApi.Authorization
{
    public class AuthorizeActionFilter : IAsyncActionFilter
    {
        private readonly PermissionItem _item;
        private readonly PermissionAction _action;
        private readonly IFunctionService _functionService;
        public AuthorizeActionFilter(PermissionItem item, PermissionAction action, IFunctionService functionService)
        {
            _item = item;
            _action = action;
            _functionService = functionService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //bool isAuthorized = MumboJumboFunction(context.HttpContext.User, _item, _action); // :)
            var functions = _functionService.GetAll();
            bool isAuthorized = true;
            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                await next();
            }
        }
    }
}
