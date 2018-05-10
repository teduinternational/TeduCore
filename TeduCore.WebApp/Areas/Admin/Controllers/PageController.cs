using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Application.Content.Pages;
using TeduCore.Application.Content.Pages.Dtos;

namespace TeduCore.WebApp.Areas.Admin.Controllers
{
    public class PageController : BaseController
    {
        public IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var model = _pageService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var model = _pageService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _pageService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(PageViewModel pageVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (pageVm.Id == Guid.Empty)
            {
                _pageService.Add(pageVm);
            }
            else
            {
                _pageService.Update(pageVm);
            }
            _pageService.SaveChanges();
            return new OkObjectResult(pageVm);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            _pageService.Delete(id);
            _pageService.SaveChanges();

            return new OkObjectResult(id);
        }
    }
}