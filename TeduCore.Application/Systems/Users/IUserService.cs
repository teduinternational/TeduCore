using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCore.Application.Systems.Users.Dtos;
using TeduCore.Application.ViewModels;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application.Systems.Users
{
    public interface IUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task DeleteAsync(Guid id);

        Task<List<AppUserViewModel>> GetAllAsync();

        PagedResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppUserViewModel> GetById(Guid id);


        Task UpdateAsync(AppUserViewModel userVm);

    }
}
