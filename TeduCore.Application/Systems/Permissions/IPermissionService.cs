using System.Collections.Generic;
using System.Threading.Tasks;
using TeduCore.Application.Systems.Permissions.Dtos;

namespace TeduCore.Application.Systems.Permissions
{
    public interface IPermissionService
    {
        ICollection<PermissionViewModel> GetByFunctionId(string functionId);

        Task<List<PermissionViewModel>> GetByUserId(string userId);

        void Add(PermissionViewModel permission);

        void DeleteAll(string functionId);

        void SaveChange();
    }
}