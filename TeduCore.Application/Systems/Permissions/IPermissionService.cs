using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeduCore.Application.Systems.Permissions.Dtos;

namespace TeduCore.Application.Systems.Permissions
{
    public interface IPermissionService
    {
        ICollection<PermissionViewModel> GetByFunctionId(Guid functionId);

        Task<List<PermissionViewModel>> GetByUserId(Guid userId);

        void Add(PermissionViewModel permission);

        void DeleteAll(Guid functionId);

        void SaveChange();
    }
}