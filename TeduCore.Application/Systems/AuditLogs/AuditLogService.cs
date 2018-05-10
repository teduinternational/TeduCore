using System;
using TeduCore.Data.Entities;
using TeduCore.Infrastructure.Interfaces;

namespace TeduCore.Application.Systems.AuditLogs
{
    public class AuditLogService : IAuditLogService
    {
        private IRepository<AuditLog, Guid> _errorRepository;
        private IUnitOfWork _unitOfWork;

        public AuditLogService(IRepository<AuditLog, Guid> errorRepository, IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Create(AuditLog error)
        {
            _errorRepository.Insert(error);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}