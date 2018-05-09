using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCore.Services.ViewModels;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Services.Interfaces
{
    public interface IRoleService
    {
        Task<bool> AddAsync(AppRoleViewModel userVm);

        Task DeleteAsync(string id);

        Task<List<AppRoleViewModel>> GetAllAsync();

        PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppRoleViewModel> GetById(string id);


        Task UpdateAsync(AppRoleViewModel userVm);

        List<PermissionViewModel> GetListFunctionWithRole(string roleId);

        void SavePermission(List<PermissionViewModel> permissions, string roleId);

        Task<bool> CheckPermission(string functionId, string action, string[] roles);
    }
}
