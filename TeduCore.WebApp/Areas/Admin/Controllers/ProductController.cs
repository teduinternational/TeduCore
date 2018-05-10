using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using TeduCore.Application.ECommerce.Products;
using TeduCore.Application.ECommerce.Products.Dtos;

namespace TeduCore.WebApp.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductController(IProductService productService, IHostingEnvironment hostingEnvironment)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var model = _productService.GetAll();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetTags(string text)
        {
            var model = _productService.GetListProductTag(text);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var model = _productService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(Guid? categoryId, string keyword, int page, int pageSize, string sortBy)
        {
            var model = _productService.GetAllPaging(categoryId, keyword, page, pageSize, sortBy);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(ProductViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (productVm.Id == Guid.Empty)
                {
                    _productService.Add(productVm);
                }
                else
                {
                    _productService.Update(productVm);
                }
                _productService.Save();
                return new OkObjectResult(productVm);
            }
        }

        [HttpPost]
        public IActionResult SaveImages(Guid productId, string[] images)
        {
            _productService.AddImages(productId, images);
            _productService.Save();
            return new OkObjectResult(images);
        }

        [HttpGet]
        public IActionResult GetImages(Guid productId)
        {
            var images = _productService.GetImages(productId);
            return new OkObjectResult(images);
        }
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _productService.Delete(id);
                _productService.Save();

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
                var listProductCategory = JsonConvert.DeserializeObject<List<Guid>>(checkedProducts);
                foreach (var item in listProductCategory)
                {
                    _productService.Delete(item);
                }

                _productService.Save();

                return new OkResult();
            }
        }

        [HttpPost]
        public IActionResult ImportExcel(IList<IFormFile> files, Guid categoryId)
        {
            if (files != null && files.Count > 0)
            {
                var file = files[0];
                var filename = ContentDispositionHeaderValue
                                   .Parse(file.ContentDisposition)
                                   .FileName
                                   .Trim('"');

                string folder = _hostingEnvironment.WebRootPath + $@"\uploaded\excels";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string filePath = Path.Combine(folder, filename);

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                _productService.ImportExcel(filePath, categoryId);
                _productService.Save();
                return new OkObjectResult(filePath);
            }
            return new NoContentResult();
        }

        [HttpPost]
        public IActionResult ExportExcel()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string directory = Path.Combine(sWebRootFolder, "export-files");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string sFileName = $"Product_{DateTime.Now:yyyyMMddhhmmss}.xlsx";
            string fileUrl = $"{Request.Scheme}://{Request.Host}/export-files/{sFileName}";
            FileInfo file = new FileInfo(Path.Combine(directory, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }
            var products = _productService.GetAll();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Products");
                worksheet.Cells["A1"].LoadFromCollection(products, true, TableStyles.Light1);
                worksheet.Cells.AutoFitColumns();
                package.Save(); //Save the workbook.
            }
            return new OkObjectResult(fileUrl);
        }
    }
}