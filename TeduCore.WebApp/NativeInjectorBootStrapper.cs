using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TeduCore.Data.EF;

using TeduCore.Data.Entities;

using TeduCore.Infrastructure.Interfaces;
using TeduCore.Services.Dapper.Implementation;
using TeduCore.Services.Dapper.Interfaces;
using TeduCore.Services.Implementation;
using TeduCore.Services.Interfaces;
using TeduCore.WebApp.Authorization;
using TeduCore.WebApp.Services;

namespace TeduCore.WebApp
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            
        }
    }
}