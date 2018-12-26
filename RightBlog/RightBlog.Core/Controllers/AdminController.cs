using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RightBlog.Core.Data;
using RightBlog.Core.Extensions;
using RightBlog.Core.Models;

namespace RightBlog.Core.Controllers
{
    public class AdminController : Controller
    {
        private readonly ArticleContext _articleContext;
        private readonly CategoryContext _categoryContext;

        public AdminController(ArticleContext articleContext, CategoryContext categoryContext)
        {
            _articleContext = articleContext;
            _categoryContext = categoryContext;
        }
        // GET: Admin
        public async Task<ActionResult> Index(ArticleFilter filter)
        {
            var articles = await _articleContext.GetArticles(filter.IsPublished,filter.Year, filter.Title);
            var model = new ArticleList { Articles = articles, Filter = filter };
            return View("Index", model);
        }

        public ActionResult CreateArticle()
        {
            return View("Article/CreateArticle");
        }
        [HttpPost]
        public async Task<ActionResult> CreateArticle(Article article, IFormFile articleImage)
        {
            if (ModelState.IsValid)
            {
                article.DatePublish = DateTime.Now;
                var id = await _articleContext.Create(article);
                if (articleImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await articleImage.CopyToAsync(memoryStream);
                        if (articleImage != null)
                        {
                            await _articleContext.StoreImage(id, memoryStream.ToArray(), articleImage.FileName);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            return View("Article/CreateArticle", article);
        }


        [HttpPost]
        public async Task<ActionResult> AttachImage(string id, IFormFile uploadedFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await uploadedFile.CopyToAsync(memoryStream);
                if (uploadedFile != null)
                {
                    await _articleContext.StoreImage(id, memoryStream.ToArray(), uploadedFile.FileName);
                }
            }
           
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> GetImage(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var image = await _articleContext.GetImage(id);
            if (image == null)
            {
                return NotFound();
            }
            return File(image, "image/png");
        }

        public async Task<ActionResult> EditArticle(string id)
        {
            Article article = await _articleContext.GetArticle(id);
            if (article == null)
                return NotFound();
            return View("Article/EditArticle",article);
        }
        [HttpPost]
        public async Task<ActionResult> EditArticle(Article article, IFormFile articleImage)
        {

           // TypeArticle.GamblingAddiction.GetDisplayName();
            if (ModelState.IsValid)
            {
                await _articleContext.Update(article);
                if (articleImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await articleImage.CopyToAsync(memoryStream);
                        if (articleImage != null)
                        {
                            await _articleContext.StoreImage(article.Id, memoryStream.ToArray(), articleImage.FileName);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            return View(article);
        }
        public async Task<ActionResult> DeleteArticle(string id)
        {
            await _articleContext.Remove(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> CategoryList()
        {
            var articles = await _categoryContext.GetCategories(null);
            return View("Category/Category", articles);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View("Category/CreateCategory");
        }
        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
               _categoryContext.Create(category);
                return RedirectToAction("CategoryList");
            }
            return View("Category/CreateCategory", category);
        }

        public async Task<ActionResult> EditCategory(string id)
        {
            Category category = await _categoryContext.GetCategory(id);
            if (category == null)
                return NotFound();
            return View("Category/EditCategory", category);
        }
        [HttpPost]
        public async Task<ActionResult> EditCategory(Category category)
        {

         //   TypeArticle.GamblingAddiction.GetDisplayName();
            if (ModelState.IsValid)
            {
                await _categoryContext.Update(category);
                return RedirectToAction("CategoryList");
            }
            return View("Category/EditCategory",category);
        }

        public async Task<ActionResult> DeleteCategory(string id)
        {
            await _categoryContext.Remove(id);
            return RedirectToAction("Index");
        }

    }
}