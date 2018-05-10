using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Data.Entities;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.Interfaces;
using TeduCore.Utilities.Constants;
using TeduCore.Utilities.Dtos;
using TeduCore.Utilities.Helpers;
using TeduCore.Application.Content.Blogs.Dtos;
using TeduCore.Application.Dtos;
using TTeduCore.Application.Content.Blogs;
using System;
using TeduCore.Data.Enums;

namespace TeduCore.Application.Content.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Post,Guid> _blogRepository;
        private readonly IRepository<Tag,string> _tagRepository;
        private readonly IRepository<PostTag, Guid> _blogTagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IRepository<Post, Guid> blogRepository,
            IRepository<PostTag, Guid> blogTagRepository,
            IRepository<Tag,string> tagRepository,
            IUnitOfWork unitOfWork)
        {
            _blogRepository = blogRepository;
            _blogTagRepository = blogTagRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }

        public BlogViewModel Add(BlogViewModel blogVm)
        {
            var blog = Mapper.Map<BlogViewModel, Post>(blogVm);

            if (!string.IsNullOrEmpty(blog.Tags))
            {
                var tags = blog.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.GetAll().Where(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = TagType.Content
                        };
                        _tagRepository.Insert(tag);
                    }

                    var blogTag = new PostTag { TagId = tagId };
                   // blog.BlogTags.Insert(blogTag);
                }
            }
            _blogRepository.Insert(blog);
            return blogVm;
        }

        public void Delete(Guid id)
        {
            _blogRepository.Delete(id);
        }

        public List<BlogViewModel> GetAll()
        {
            return _blogRepository.GetAll()
                .ProjectTo<BlogViewModel>().ToList();
        }

        public PagedResult<BlogViewModel> GetAllPaging(string keyword, int pageSize, int page = 1)
        {
            var query = _blogRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<BlogViewModel>()
            {
                Results = data.ProjectTo<BlogViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize,
            };

            return paginationSet;
        }

        public BlogViewModel GetById(Guid id)
        {
            return Mapper.Map<Post, BlogViewModel>(_blogRepository.Get(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(BlogViewModel blog)
        {
            _blogRepository.Update(Mapper.Map<BlogViewModel, Post>(blog));
            if (!string.IsNullOrEmpty(blog.Tags))
            {
                string[] tags = blog.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.GetAll().Where(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = Data.Enums.TagType.Product
                        };
                        _tagRepository.Insert(tag);
                    }
                    _blogTagRepository.Delete(x => x.Id == blog.Id);
                    PostTag blogTag = new PostTag
                    {
                        PostId = blog.Id,
                        TagId = tagId
                    };
                    _blogTagRepository.Insert(blogTag);
                }
            }
        }

        public List<BlogViewModel> GetLastest(int top)
        {
            return _blogRepository.GetAll().Where(x => x.Status == Status.Actived).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<BlogViewModel>().ToList();
        }

        public List<BlogViewModel> GetHotProduct(int top)
        {
            return _blogRepository.GetAll().Where(x => x.Status == Status.Actived && x.HotFlag == true)
                .OrderByDescending(x => x.DateCreated)
                .Take(top)
                .ProjectTo<BlogViewModel>()
                .ToList();
        }

        public List<BlogViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow)
        {
            var query = _blogRepository.GetAll().Where(x => x.Status == Status.Actived);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BlogViewModel>().ToList();
        }

        public List<string> GetListByName(string name)
        {
            return _blogRepository.GetAll().Where(x => x.Status == Status.Actived
            && x.Name.Contains(name)).Select(y => y.Name).ToList();
        }

        public List<BlogViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _blogRepository.GetAll().Where(x => x.Status == Status.Actived
            && x.Name.Contains(keyword));

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BlogViewModel>()
                .ToList();
        }

        public List<BlogViewModel> GetReatedBlogs(Guid id, int top)
        {
            return _blogRepository.GetAll().Where(x => x.Status == Status.Actived
                && x.Id != id)
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ProjectTo<BlogViewModel>()
            .ToList();
        }

        public List<TagViewModel> GetListTagById(Guid id)
        {
            //return _blogTagRepository.GetAll().Where(x => x.PostId == id)
            //    .Select(y => y.Tag)
            //    .ProjectTo<TagViewModel>()
            //    .ToList();
            throw new NotImplementedException();
        }

        public void IncreaseView(Guid id)
        {
            var product = _blogRepository.Get(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
        }

        public List<BlogViewModel> GetListByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in _blogRepository.GetAll()
                        join pt in _blogTagRepository.GetAll()
                        on p.Id equals pt.PostId
                        where pt.TagId == tagId && p.Status == Status.Actived
                        orderby p.DateCreated descending
                        select p;

            totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var model = query.ProjectTo<BlogViewModel>();
            return model.ToList();
        }

        public TagViewModel GetTag(string tagId)
        {
            return Mapper.Map<Tag, TagViewModel>(_tagRepository.FirstOrDefault(x => x.Id == tagId));
        }

        public List<BlogViewModel> GetList(string keyword)
        {
            var query = !string.IsNullOrEmpty(keyword) ?
                _blogRepository.GetAll().Where(x => x.Name.Contains(keyword)).ProjectTo<BlogViewModel>()
                : _blogRepository.GetAll().ProjectTo<BlogViewModel>();
            return query.ToList();
        }

        public List<TagViewModel> GetListTag(string searchText)
        {
            return _tagRepository.GetAll().Where(x => x.Type == Data.Enums.TagType.Content
            && searchText.Contains(x.Name)).ProjectTo<TagViewModel>().ToList();
        }
    }
}