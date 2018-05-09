using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Services.Interfaces;
using TeduCore.Services.ViewModels.Blog;
using TeduCore.Utilities.Helpers;

namespace TeduCore.WebApp.Areas.Admin.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var model = _blogService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetTags(string text)
        {
            var model = _blogService.GetListTag(text);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _blogService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _blogService.GetAllPaging(keyword, pageSize, page);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(BlogViewModel blogVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                blogVm.SeoAlias = TextHelper.ToUnsignString(blogVm.Name);
                if (blogVm.Id == 0)
                {
                    _blogService.Add(blogVm);
                }
                else
                {
                    _blogService.Update(blogVm);
                }
                _blogService.Save();
                return new OkObjectResult(blogVm);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _blogService.Delete(id);
                _blogService.Save();

                return new OkObjectResult(id);
            }
        }

        [HttpDelete]
        public IActionResult DeleteMulti(string checkedProducts)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                var listProductCategory = JsonConvert.DeserializeObject<List<int>>(checkedProducts);
                foreach (var item in listProductCategory)
                {
                    _blogService.Delete(item);
                }

                _blogService.Save();

                return new OkResult();
            }
        }
    }
}