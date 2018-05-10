using System;
using System.Collections.Generic;
using TeduCore.Application.Content.Blogs.Dtos;
using TeduCore.Application.Dtos;
using TeduCore.Utilities.Dtos;

namespace TTeduCore.Application.Content.Blogs
{
    public interface IBlogService
    {
        BlogViewModel Add(BlogViewModel product);

        void Update(BlogViewModel product);

        void Delete(Guid id);

        List<BlogViewModel> GetAll();

        PagedResult<BlogViewModel> GetAllPaging(string keyword,int pageSize, int page);

        List<BlogViewModel> GetLastest(int top);

        List<BlogViewModel> GetHotProduct(int top);

        List<BlogViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow);

        List<BlogViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        List<BlogViewModel> GetList(string keyword);

        List<BlogViewModel> GetReatedBlogs(Guid id, int top);

        List<string> GetListByName(string name);

        BlogViewModel GetById(Guid id);

        void Save();

        List<TagViewModel> GetListTagById(Guid id);

        TagViewModel GetTag(string tagId);

        void IncreaseView(Guid id);

        List<BlogViewModel> GetListByTag(string tagId, int page, int pagesize, out int totalRow);

        List<TagViewModel> GetListTag(string searchText);
    }
}