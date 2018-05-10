using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduCore.Application.ECommerce.Bills;
using TeduCore.Application.ECommerce.Bills.Dtos;
using TeduCore.Application.ECommerce.Products;
using TeduCore.Data.Enums;
using TeduCore.Utilities.Constants;
using TeduCore.WebApp.Extensions;
using TeduCore.WebApp.Models;
using TeduCore.WebApp.Services;

namespace TeduCore.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBillService _billService;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IViewRenderService _viewRenderService;

        public CartController(IProductService productService,
            IEmailSender emailSender,
            IConfiguration configuration,
            IViewRenderService viewRenderService,
             IBillService billService)
        {
            _configuration = configuration;
            _productService = productService;
            _billService = billService;
            _emailSender = emailSender;
            _viewRenderService = viewRenderService;
        }

        [Route("gio-hang.html", Name = "Cart")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("dat-hang.html", Name = "Checkout")]
        [HttpGet]
        public IActionResult Checkout()
        {
            var model = new CheckoutViewModel();
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session == null)
                return Redirect("/gio-hang.html");
            model.Carts = session;
            return View(model);
        }

        [HttpPost]
        [Route("dat-hang.html", Name = "Checkout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);

            if (ModelState.IsValid)
            {
                if (session != null)
                {
                    var details = new List<BillDetailViewModel>();
                    foreach (var item in session)
                    {
                        details.Add(new BillDetailViewModel()
                        {
                            Product = item.Product,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            ProductId = item.Product.Id
                        });
                    }
                    var billViewModel = new BillViewModel()
                    {
                        CustomerMobile = model.CustomerMobile,
                        BillStatus = BillStatus.New,
                        CustomerAddress = model.CustomerAddress,
                        CustomerName = model.CustomerName,
                        CustomerMessage = model.CustomerMessage,
                        BillDetails = details,
                        CustomerId = User.Identity.IsAuthenticated == true
                            ? ((ClaimsIdentity)User.Identity).GetSpecificClaim("UserId")
                            : null
                    };
                    _billService.Create(billViewModel);
                    try
                    {
                        _billService.Save();
                        var content = await _viewRenderService.RenderToStringAsync("Cart/_OrderMail", billViewModel);
                        //Send mail
                        await _emailSender.SendEmailAsync(_configuration["MailSettings:AdminMail"], "Đơn hàng mới", content);
                        ViewData["Success"] = true;
                    }
                    catch (Exception ex)
                    {
                        ViewData["Success"] = false;
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }
            model.Carts = session;
            return View(model);
        }

        #region Ajax request

        /// <summary>
        /// Get list item
        /// </summary>
        /// <returns></returns>
        public IActionResult GetCart()
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session == null)
                session = new List<ShoppingCartViewModel>();
            return new OkObjectResult(session);
        }

        /// <summary>
        /// Remove all products in cart
        /// </summary>
        /// <returns></returns>
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CommonConstants.CartSession);
            return new OkObjectResult("OK");
        }

        /// <summary>
        /// Update product quantity
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public IActionResult UpdateCart(Guid productId, int quantity)
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session != null)
            {
                bool hasChanged = false;
                foreach (var item in session)
                {
                    if (item.Product.Id == productId)
                    {
                        var product = _productService.GetById(productId);
                        item.Product = product;
                        item.Quantity = quantity;
                        item.Price = product.PromotionPrice ?? product.Price;
                        hasChanged = true;
                    }
                }
                if (hasChanged)
                {
                    HttpContext.Session.Set(CommonConstants.CartSession, session);
                }
                return new OkObjectResult(productId);
            }
            return new EmptyResult();
        }

        /// <summary>
        /// Remove a product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult RemoveFromCart(Guid productId)
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session != null)
            {
                bool hasChanged = false;
                foreach (var item in session)
                {
                    if (item.Product.Id == productId)
                    {
                        session.Remove(item);
                        hasChanged = true;
                        break;
                    }
                }
                if (hasChanged)
                {
                    HttpContext.Session.Set(CommonConstants.CartSession, session);
                }
                return new OkObjectResult(productId);
            }
            return new EmptyResult();
        }

        /// <summary>
        /// Add product to cart
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddToCart(Guid productId, int quantity)
        {
            //Get product detail
            var product = _productService.GetById(productId);

            //Get session with item list from cart
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.CartSession);
            if (session != null)
            {
                //Convert string to list object
                bool hasChanged = false;

                //Check exist with item product id
                if (session.Any(x => x.Product.Id == productId))
                {
                    foreach (var item in session)
                    {
                        //Update quantity for product if match product id
                        if (item.Product.Id == productId)
                        {
                            item.Quantity += quantity;
                            item.Price = product.PromotionPrice ?? product.Price;
                            hasChanged = true;
                        }
                    }
                }
                else
                {
                    session.Add(new ShoppingCartViewModel()
                    {
                        Product = product,
                        Quantity = quantity,
                        Price = product.PromotionPrice ?? product.Price
                    });
                    hasChanged = true;
                }

                //Update back to cart
                if (hasChanged)
                {
                    HttpContext.Session.Set(CommonConstants.CartSession, session);
                }
            }
            else
            {
                //Add new cart
                var cart = new List<ShoppingCartViewModel>();
                cart.Add(new ShoppingCartViewModel()
                {
                    Product = product,
                    Quantity = quantity,
                    Price = product.PromotionPrice ?? product.Price
                });
                HttpContext.Session.Set(CommonConstants.CartSession, cart);
            }
            return new OkObjectResult(productId);
        }

        #endregion Ajax request
    }
}