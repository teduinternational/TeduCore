using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduCore.Application.Systems.Functions.Dtos;
using TeduCore.Application.Systems.Roles.Dtos;

namespace TeduCore.Application.Systems.Permissions.Dtos
{
    public class PermissionViewModel
    {

        public Guid Id { get; set; }


        public Guid RoleId { get; set; }

        public Guid FunctionId { get; set; }

        public bool CanCreate { set; get; }

        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }

        public bool CanDelete { set; get; }

    }
}