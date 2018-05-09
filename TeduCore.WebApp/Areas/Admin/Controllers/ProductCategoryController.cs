using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Services.Interfaces;
using TeduCore.Services.ViewModels;
using TeduCore.Utilities.Helpers;

namespace TeduCore.WebApp.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        #region Initialize

        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        #endregion Initialize

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllFillter(string filter)
        {
            var model = _productCategoryService.GetAll();
            return new ObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _productCategoryService.GetAll();
            return new ObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _productCategoryService.GetById(id);

            return new ObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(ProductCategoryViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            if (productVm.Id == 0)
            {
                _productCategoryService.Add(productVm);
            }
            else
            {
                _productCategoryService.Update(productVm);
            }
            _productCategoryService.Save();
            return new OkObjectResult(productVm);
        }

        [HttpPost]
        public IActionResult UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            if (sourceId == targetId)
            {
                return new BadRequestResult();
            }
            _productCategoryService.UpdateParentId(sourceId, targetId, items);
            _productCategoryService.Save();
            return new OkResult();
        }

        [HttpPost]
        public IActionResult ReOrder(int sourceId, int targetId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            if (sourceId == targetId)
            {
                return new BadRequestResult();
            }
            _productCategoryService.ReOrder(sourceId, targetId);
            _productCategoryService.Save();
            return new OkResult();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            _productCategoryService.Delete(id);
            _productCategoryService.Save();
            return new OkResult();
        }

        [HttpPost]
        public IActionResult DeleteMulti(string checkedProductCategories)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            var listProductCategory = JsonConvert.DeserializeObject<List<int>>(checkedProductCategories);
            foreach (var item in listProductCategory)
            {
                _productCategoryService.Delete(item);
            }

            _productCategoryService.Save();

            return new OkResult();
        }
    }
}