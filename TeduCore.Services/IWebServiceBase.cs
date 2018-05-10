using System.Collections.Generic;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Services
{
    public interface IWebServiceBase<T, K, VM> where VM : class
        where T : class
    {
        void Create(VM viewModel);

        void Update(VM viewModel);

        void Delete(K id);

        T Get(K id);

        List<VM> GetAll();

        PagedResult<VM> GetAllPaging(string keyword, int pageSize, int page);

        void Save();
    }
}