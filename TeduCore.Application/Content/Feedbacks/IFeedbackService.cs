using System;
using System.Collections.Generic;
using TeduCore.Application.Content.Feedbacks.Dtos;
using TeduCore.Data.Entities;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application.Content.Feedbacks
{
    public interface IFeedbackService : IWebServiceBase<Feedback,Guid,FeedbackViewModel>
    {
        PagedResult<FeedbackViewModel> GetAllPaging(string keyword, int page, int pageSize);
    }
}