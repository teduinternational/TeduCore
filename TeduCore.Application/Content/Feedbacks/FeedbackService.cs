using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Application.Content.Feedbacks.Dtos;
using TeduCore.Data.Entities;
using TeduCore.Infrastructure.Interfaces;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application.Content.Feedbacks
{
    public class FeedbackService : IFeedbackService
    {
        private IRepository<Feedback, Guid> _feedbackRepository;
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IRepository<Feedback, Guid> feedbackRepository,
            IUnitOfWork unitOfWork)
        {
            _feedbackRepository = feedbackRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(FeedbackViewModel feedbackVm)
        {
            var page = Mapper.Map<FeedbackViewModel, Feedback>(feedbackVm);
            _feedbackRepository.Insert(page);
        }

        public void Delete(Guid id)
        {
            _feedbackRepository.Delete(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<FeedbackViewModel> GetAll()
        {
            return _feedbackRepository.GetAll().ProjectTo<FeedbackViewModel>().ToList();
        }

        public PagedResult<FeedbackViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _feedbackRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<FeedbackViewModel>()
            {
                Results = data.ProjectTo<FeedbackViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public FeedbackViewModel GetById(Guid id)
        {
            return Mapper.Map<Feedback, FeedbackViewModel>(_feedbackRepository.GetById(id));
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(FeedbackViewModel feedbackVm)
        {
            var page = Mapper.Map<FeedbackViewModel, Feedback>(feedbackVm);
            _feedbackRepository.Update(page);
        }
    }
}