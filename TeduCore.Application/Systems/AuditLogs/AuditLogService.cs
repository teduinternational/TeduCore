using TeduCore.Data.Entities;
using TeduCore.Infrastructure.Interfaces;

namespace TeduCore.Application.Systems.AuditLogs
{
    public class AuditLogService : IAuditLogService
    {
        private IRepository<Error, int> _errorRepository;
        private IUnitOfWork _unitOfWork;

        public AuditLogService(IRepository<Error, int> errorRepository, IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Create(Error error)
        {
            _errorRepository.Add(error);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}