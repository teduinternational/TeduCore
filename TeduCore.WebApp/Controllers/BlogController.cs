﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using TeduCore.WebApp.Models;
using TTeduCore.Application.Content.Blogs;

namespace TeduCore.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IConfiguration _configuration;

        public BlogController(IBlogService blogService, IConfiguration configuration)
        {
            _blogService = blogService;
            _configuration = configuration;
        }

        [Route("bai-viet.html", Name = "blogs")]
        public IActionResult Index(int page = 1)
        {
            var pageSize = _configuration.GetValue<int>("PageSize");
            var model = _blogService.GetAllPaging(string.Empty, pageSize, page);
            return View(model);
        }

        [Route("{alias}-b.{id}.html")]
        public IActionResult Details(Guid id)
        {
            var model = new BlogDetailViewModel();
            model.Blog = _blogService.GetById(id);
            model.MostBlogs = _blogService.GetLastest(6);
            return View(model);
        }
    }
}