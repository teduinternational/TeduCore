using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCore.Data.Interfaces
{
    interface IMustHaveTenant
    {
      Guid TenantId { get; set; }

    }
}
