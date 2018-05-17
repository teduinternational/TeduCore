using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeduCore.Application.Systems.Functions.Dtos;
using TeduCore.Application.ViewModels;
using TeduCore.Data.Entities;

namespace TeduCore.Application.Systems.Functions
{
    public interface IFunctionService : IWebServiceBase<Function, Guid, FunctionViewModel>
    {
        Task<List<FunctionViewModel>> GetAll(string filter);

        Task<List<FunctionViewModel>> GetAllWithPermission(string userName);

        List<FunctionViewModel> GetAllWithParentId(Guid? parentId);

        bool CheckExistedId(Guid id);

        void UpdateParentId(Guid sourceId, Guid targetId, Dictionary<Guid, int> items);

        void ReOrder(Guid sourceId, Guid targetId);
    }
}