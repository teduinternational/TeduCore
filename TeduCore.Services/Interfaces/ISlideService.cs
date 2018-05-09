using System.Collections.Generic;
using TeduCore.Services.ViewModels;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Services.Interfaces
{
    public interface ISlideService
    {
        void Add(SlideViewModel slideVm);

        void Update(SlideViewModel slideVm);

        void Delete(int id);

        List<SlideViewModel> GetAll();

        PagedResult<SlideViewModel> GetAllPaging(string keyword, int page, int pageSize, string sortBy);

        SlideViewModel GetById(int id);

        void SaveChanges();
    }
}