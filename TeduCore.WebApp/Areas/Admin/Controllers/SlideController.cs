using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Application.Content.Slides;
using TeduCore.Application.Content.Slides.Dtos;

namespace TeduCore.WebApp.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        private readonly ISlideService _slideService;

        public SlideController(ISlideService slideService)
        {
            _slideService = slideService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var model = _slideService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var model = _slideService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize, string sortBy)
        {
            var model = _slideService.GetAllPaging(keyword, page, pageSize, sortBy);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(SlideViewModel slideVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (slideVm.Id == Guid.Empty)
            {
                _slideService.Add(slideVm);
            }
            else
            {
                _slideService.Update(slideVm);
            }
            _slideService.SaveChanges();
            return new OkObjectResult(slideVm);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            _slideService.Delete(id);
            _slideService.SaveChanges();

            return new OkObjectResult(id);
        }

        [HttpDelete]
        public IActionResult DeleteMulti(string checkedProducts)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            var listProductCategory = JsonConvert.DeserializeObject<List<Guid>>(checkedProducts);
            foreach (var item in listProductCategory)
            {
                _slideService.Delete(item);
            }

            _slideService.SaveChanges();

            return new OkResult();
        }
    }
}