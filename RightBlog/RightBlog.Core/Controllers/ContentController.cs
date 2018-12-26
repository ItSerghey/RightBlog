using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RightBlog.Core.Data;
using RightBlog.Core.Models;

namespace RightBlog.Core.Controllers
{
    public class ContentController : Controller
    {
        private readonly ArticleContext _articleContext;

        public ContentController(ArticleContext articleContext)
        {
            _articleContext = articleContext;
        }

        // GET: TestModels
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> GetArticles()
        {
            var articles = await _articleContext.GetArticles(true,null, null);
            //  var model = new ComputerList { Computers = computers, Filter = filter };

            return View("ContentPreview", articles);
        }

        public async Task<ActionResult> GetImage(string id)
        {
            var image = await _articleContext.GetImage(id);
            if (image == null)
            {
                return NotFound();
            }
            return File(image, "image/png");
        }

[HttpGet("content/article/{seoUrl}", Name = "Article")]
        public async Task<ActionResult> Article(string seoUrl)
        {
           var article = await _articleContext.GetArticleBySeoUrl(seoUrl);
            return View("Article", article);
        }

    }
}
