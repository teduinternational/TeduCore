using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Application.Content.Posts.Dtos;
using TeduCore.Application.Dtos;
using TeduCore.Data.Entities;
using TeduCore.Data.Enums;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.Interfaces;
using TeduCore.Utilities.Dtos;
using TeduCore.Utilities.Helpers;
using TeduCore.Application.Content.Posts;

namespace TeduCore.Application.Content.Posts
{
    public class PostService : WebServiceBase<Post, Guid, PostViewModel>, IPostService
    {
        private readonly IRepository<Post, Guid> _blogRepository;
        private readonly IRepository<Tag, string> _tagRepository;
        private readonly IRepository<PostTag, Guid> _blogTagRepository;

        public PostService(IRepository<Post, Guid> blogRepository,
            IRepository<PostTag, Guid> blogTagRepository,
            IRepository<Tag, string> tagRepository,
            IUnitOfWork unitOfWork) : base(blogRepository, unitOfWork)
        {
            _blogRepository = blogRepository;
            _blogTagRepository = blogTagRepository;
            _tagRepository = tagRepository;
        }

        public override void Add(PostViewModel blogVm)
        {
            var blog = Mapper.Map<PostViewModel, Post>(blogVm);
            blog.Id = Guid.NewGuid();
            if (!string.IsNullOrEmpty(blog.Tags))
            {
                var tags = blog.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (_tagRepository.Count(x => x.Id == tagId) == 0)
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = TagType.Content
                        };
                        _tagRepository.Insert(tag);
                    }

                    var blogTag = new PostTag { TagId = tagId, PostId = blog.Id };
                    _blogTagRepository.Insert(blogTag);
                }
            }
            _blogRepository.Insert(blog);
        }

        public PagedResult<PostViewModel> GetAllPaging(string keyword, int pageSize, int page = 1)
        {
            var query = _blogRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<PostViewModel>()
            {
                Results = data.ProjectTo<PostViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize,
            };

            return paginationSet;
        }

        public override void Update(PostViewModel blog)
        {
            _blogRepository.Update(Mapper.Map<PostViewModel, Post>(blog));
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

        public List<PostViewModel> GetLastest(int top)
        {
            return _blogRepository.GetAll().Where(x => x.Status == Status.Actived).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<PostViewModel>().ToList();
        }

        public List<PostViewModel> GetHotProduct(int top)
        {
            return _blogRepository.GetAll().Where(x => x.Status == Status.Actived && x.HotFlag == true)
                .OrderByDescending(x => x.DateCreated)
                .Take(top)
                .ProjectTo<PostViewModel>()
                .ToList();
        }

        public List<PostViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow)
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
                .ProjectTo<PostViewModel>().ToList();
        }

        public List<string> GetListByName(string name)
        {
            return _blogRepository.GetAll().Where(x => x.Status == Status.Actived
            && x.Name.Contains(name)).Select(y => y.Name).ToList();
        }

        public List<PostViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
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
                .ProjectTo<PostViewModel>()
                .ToList();
        }

        public List<PostViewModel> GetReatedBlogs(Guid id, int top)
        {
            return _blogRepository.GetAll().Where(x => x.Status == Status.Actived
                && x.Id != id)
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ProjectTo<PostViewModel>()
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
            var product = _blogRepository.GetById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
        }

        public List<PostViewModel> GetListByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in _blogRepository.GetAll()
                        join pt in _blogTagRepository.GetAll()
                        on p.Id equals pt.PostId
                        where pt.TagId == tagId && p.Status == Status.Actived
                        orderby p.DateCreated descending
                        select p;

            totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var model = query.ProjectTo<PostViewModel>();
            return model.ToList();
        }

        public TagViewModel GetTag(string tagId)
        {
            return Mapper.Map<Tag, TagViewModel>(_tagRepository.FirstOrDefault(x => x.Id == tagId));
        }

        public List<PostViewModel> GetList(string keyword)
        {
            var query = !string.IsNullOrEmpty(keyword) ?
                _blogRepository.GetAll().Where(x => x.Name.Contains(keyword)).ProjectTo<PostViewModel>()
                : _blogRepository.GetAll().ProjectTo<PostViewModel>();
            return query.ToList();
        }

        public List<TagViewModel> GetListTag(string searchText)
        {
            return _tagRepository.GetAll().Where(x => x.Type == Data.Enums.TagType.Content
            && searchText.Contains(x.Name)).ProjectTo<TagViewModel>().ToList();
        }
    }
}