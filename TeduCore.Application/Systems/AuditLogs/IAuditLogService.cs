using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Data.Entities;

namespace TeduCore.Application.Systems.AuditLogs
{
    public interface IAuditLogService
    {
        void Create(AuditLog error);

        void Save();
    }
}
