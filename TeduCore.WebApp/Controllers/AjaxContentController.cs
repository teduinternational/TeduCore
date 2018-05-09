using Microsoft.AspNetCore.Mvc;

namespace TeduCore.WebApp.Controllers
{
    public class AjaxContentController : Controller
    {
        public IActionResult HeaderCart()
        {
            return ViewComponent("HeaderCart");
        }

        public IActionResult MyCart()
        {
            return ViewComponent("MyCart");
        }
    }
}