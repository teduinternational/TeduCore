using System;
using System.Collections.Generic;
using TeduCore.Application.Content.Feedbacks.Dtos;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application.Content.Feedbacks
{
    public interface IFeedbackService
    {
        void Add(FeedbackViewModel feedbackVm);

        void Update(FeedbackViewModel feedbackVm);

        void Delete(Guid id);

        List<FeedbackViewModel> GetAll();

        PagedResult<FeedbackViewModel> GetAllPaging(string keyword, int page, int pageSize);

        FeedbackViewModel GetById(Guid id);

        void SaveChanges();
    }
}