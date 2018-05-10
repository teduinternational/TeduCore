using System;
using System.Collections.Generic;
using TeduCore.Application.Content.Pages.Dtos;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application.Content.Pages
{
    public interface IPageService : IDisposable
    {
        void Add(PageViewModel pageVm);

        void Update(PageViewModel pageVm);

        void Delete(Guid id);

        List<PageViewModel> GetAll();

        PagedResult<PageViewModel> GetAllPaging(string keyword, int page, int pageSize);

        PageViewModel GetByAlias(string alias);

        PageViewModel GetById(Guid id);

        void SaveChanges();
    }
}