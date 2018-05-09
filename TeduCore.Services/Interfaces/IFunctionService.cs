using System.Collections.Generic;
using System.Threading.Tasks;
using TeduCore.Services.ViewModels;

namespace TeduCore.Services.Interfaces
{
    public interface IFunctionService
    {
        void Add(FunctionViewModel function);

        Task<List<FunctionViewModel>> GetAll(string filter);

        Task<List<FunctionViewModel>> GetAllWithPermission(string userName);

        IEnumerable<FunctionViewModel> GetAllWithParentId(string parentId);

        FunctionViewModel GetById(string id);

        void Update(FunctionViewModel function);

        void Delete(string id);

        void Save();

        bool CheckExistedId(string id);

        void UpdateParentId(string sourceId, string targetId, Dictionary<string, int> items);

        void ReOrder(string sourceId, string targetId);
    }
}