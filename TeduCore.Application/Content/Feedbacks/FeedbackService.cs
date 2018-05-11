using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using TeduCore.Application.Content.Feedbacks.Dtos;
using TeduCore.Data.Entities;
using TeduCore.Infrastructure.Interfaces;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application.Content.Feedbacks
{
    public class FeedbackService : WebServiceBase<Feedback, Guid, FeedbackViewModel>, IFeedbackService
    {
        private IRepository<Feedback, Guid> _feedbackRepository;

        public FeedbackService(IRepository<Feedback, Guid> feedbackRepository,
            IUnitOfWork unitOfWork) : base(feedbackRepository, unitOfWork)
        {
            _feedbackRepository = feedbackRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
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
    }
}