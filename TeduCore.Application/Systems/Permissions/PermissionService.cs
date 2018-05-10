using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCore.Application.Systems.Permissions.Dtos;
using TeduCore.Data.Entities;
using TeduCore.Infrastructure.Interfaces;

namespace TeduCore.Application.Systems.Permissions
{
    public class PermissionService : IPermissionService
    {
        private IRepository<Function, Guid> _functionRepository;
        private IRepository<Permission, Guid> _permissionRepository;
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;
        private IUnitOfWork _unitOfWork;

        public PermissionService(IRepository<Permission, Guid> permissionRepository,
              RoleManager<AppRole> roleManager,
              UserManager<AppUser> userManager,
            IRepository<Function, Guid> functionRepository, IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _functionRepository = functionRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public void Add(PermissionViewModel permissionVm)
        {
            var permission = Mapper.Map<PermissionViewModel, Permission>(permissionVm);
            _permissionRepository.Insert(permission);
        }

        public void DeleteAll(Guid functionId)
        {
            _permissionRepository.Delete(x => x.FunctionId == functionId);
        }

        public ICollection<PermissionViewModel> GetByFunctionId(Guid functionId)
        {
            return _permissionRepository
                .GetAll().Where(x => x.FunctionId == functionId)
                .ProjectTo<PermissionViewModel>().ToList();
        }

        public async Task<List<PermissionViewModel>> GetByUserId(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var roles = await _userManager.GetRolesAsync(user);

            var query = (from f in _functionRepository.GetAll()
                         join p in _permissionRepository.GetAll() on f.Id equals p.FunctionId
                         join r in _roleManager.Roles on p.RoleId equals r.Id
                         where roles.Contains(r.Name)
                         select p);

            return query.ProjectTo<PermissionViewModel>().ToList();
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}