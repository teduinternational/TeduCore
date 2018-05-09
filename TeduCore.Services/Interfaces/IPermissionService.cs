using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCore.Data.Entities;

namespace TeduCore.Services.Interfaces
{
    public interface IPermissionService
    {
        ICollection<Permission> GetByFunctionId(string functionId);
        Task<List<Permission>> GetByUserId(string userId);
        void Add(Permission permission);
        void DeleteAll(string functionId);
        void SaveChange();
    }
}
