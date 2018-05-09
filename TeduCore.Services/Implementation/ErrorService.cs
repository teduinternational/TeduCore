using TeduCore.Data.Entities;

using TeduCore.Infrastructure.Interfaces;
using TeduCore.Services.Interfaces;

namespace TeduCore.Services.Implementation
{
    public class ErrorService : IErrorService
    {
        private IRepository<Error, int> _errorRepository;
        private IUnitOfWork _unitOfWork;

        public ErrorService(IRepository<Error, int> errorRepository, IUnitOfWork unitOfWork)
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