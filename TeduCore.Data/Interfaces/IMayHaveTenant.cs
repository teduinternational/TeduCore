using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCore.Data.Interfaces
{
    public interface IMayHaveTenant
    {
        Guid? TenantId { get; set; }
    }
}
