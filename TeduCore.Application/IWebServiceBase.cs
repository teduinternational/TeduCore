using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.SharedKernel;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application
{
    public interface IWebServiceBase<TEntity,TPrimaryKey,ViewModel> where ViewModel : class
        where TEntity : DomainEntity<TPrimaryKey>
    {
        void Add(ViewModel viewModel);

        void Update(ViewModel viewModel);

        void Delete(TPrimaryKey id);

        ViewModel GetById(TPrimaryKey id);

        List<ViewModel> GetAll();

        PagedResult<ViewModel> GetAllPaging(Expression<Func<TEntity, bool>> predicate, Func<TEntity, bool> orderBy,
            SortDirection sortDirection, int pageIndex, int pageSize);

        void Save();
    }
}