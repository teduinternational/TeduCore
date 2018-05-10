using System;
using System.Collections.Generic;
using TeduCore.Application.Content.Slides.Dtos;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application.Content.Slides
{
    public interface ISlideService
    {
        void Add(SlideViewModel slideVm);

        void Update(SlideViewModel slideVm);

        void Delete(Guid id);

        List<SlideViewModel> GetAll();

        PagedResult<SlideViewModel> GetAllPaging(string keyword, int page, int pageSize, string sortBy);

        SlideViewModel GetById(Guid id);

        void SaveChanges();
    }
}