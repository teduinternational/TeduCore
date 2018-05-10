using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCore.Data.Entities;

using TeduCore.Infrastructure.Interfaces;
using TeduCore.Services.Interfaces;

namespace TeduCore.Services.Implementation
{
    public class PermissionService : IPermissionService
    {
        private IRepository<Function, string> _functionRepository;
        private IRepository<Permission, int> _permissionRepository;
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;
        private IUnitOfWork _unitOfWork;

        public PermissionService(IRepository<Permission, int> permissionRepository,
              RoleManager<AppRole> roleManager,
              UserManager<AppUser> userManager,
            IRepository<Function, string> functionRepository, IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _functionRepository = functionRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public void Add(Permission permission)
        {
            _permissionRepository.Add(permission);
        }

        public void DeleteAll(string functionId)
        {
            var permissions = _permissionRepository.FindAll(x => x.FunctionId == functionId).ToList();
            _permissionRepository.RemoveMultiple(permissions);
        }

        public ICollection<Permission> GetByFunctionId(string functionId)
        {
            return _permissionRepository
                .FindAll(x => x.FunctionId == functionId, x => x.AppRole).ToList();
        }

        public async Task<List<Permission>> GetByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            var query = (from f in _functionRepository.FindAll()
                         join p in _permissionRepository.FindAll() on f.Id equals p.FunctionId
                         join r in _roleManager.Roles on p.RoleId equals r.Id
                         where roles.Contains(r.Name)
                         select p);

            return query.ToList();
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}