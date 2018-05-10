using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCore.Application.Systems.Functions;
using TeduCore.Application.Systems.Functions.Dtos;

namespace TeduCore.WebApp.Areas.Admin.Controllers
{
    public class FunctionController : BaseController
    {
        #region Initialize

        private IFunctionService _functionService;

        public FunctionController(IFunctionService functionService)
        {
            this._functionService = functionService;
        }

        #endregion Initialize

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllFillter(string filter)
        {
            var model = _functionService.GetAll(filter);
            return new ObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await _functionService.GetAll(string.Empty);
            var parentFunctions = model.Where(c => c.ParentId == null);
            var items = new List<FunctionViewModel>();
            foreach (var function in parentFunctions)
            {
                //add the parent category to the item list
                items.Add(function);
                //now get all its children (separate Category in case you need recursion)
                GetByParentId(model.ToList(), function, items);
            }
            return new ObjectResult(items);
        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var model = _functionService.GetById(id);

            return new ObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(FunctionViewModel functionVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (functionVm.Id == Guid.Empty)
                {
                    _functionService.Add(functionVm);
                }
                else
                {
                    _functionService.Update(functionVm);
                }
                _functionService.Save();
                return new OkObjectResult(functionVm);
            }
        }

        [HttpPost]
        public IActionResult UpdateParentId(Guid sourceId, Guid targetId, Dictionary<Guid, int> items)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _functionService.UpdateParentId(sourceId, targetId, items);
                    _functionService.Save();
                    return new OkResult();
                }
            }
        }

        [HttpPost]
        public IActionResult ReOrder(Guid sourceId, Guid targetId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _functionService.ReOrder(sourceId, targetId);
                    _functionService.Save();
                    return new OkObjectResult(sourceId);
                }
            }
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            else
            {
                _functionService.Delete(id);
                _functionService.Save();
                return new OkObjectResult(id);
            }
        }

        #region Private Functions

        private void GetByParentId(IEnumerable<FunctionViewModel> allCats,
            FunctionViewModel parent, IList<FunctionViewModel> items)
        {
            var categoryEntities = allCats as FunctionViewModel[] ?? allCats.ToArray();
            var subCats = categoryEntities.Where(c => c.ParentId == parent.Id);
            foreach (var cat in subCats)
            {
                //add this category
                items.Add(cat);
                //recursive call in case your have a hierarchy more than 1 level deep
                GetByParentId(categoryEntities, cat, items);
            }
        }

        #endregion Private Functions
    }
}