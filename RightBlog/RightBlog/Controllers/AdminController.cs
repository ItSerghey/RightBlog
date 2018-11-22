using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RightBlog.Context;
using RightBlog.Models;

namespace RightBlog.Controllers
{
    public class AdminController : Controller
    {
        private readonly ArticleContext db = new ArticleContext();
        // GET: Admin
        public async Task<ActionResult> Index(ArticleFilter filter)
        {
            var articles = await db.GetArticles(filter.Year, filter.Title);
            var model = new ArticleList { Articles = articles, Filter = filter };
            return View("Index",model);
        }

        public ActionResult CreateArticle()
        {
            return View("CreateArticle");
        }
        [HttpPost]
        public async Task<ActionResult> CreateArticle(Article article, HttpPostedFileBase articleImage)
        {
            if (ModelState.IsValid)
            {
                article.DatePublish = DateTime.Now;
              var id =  await db.Create(article);
                if (articleImage != null)
                {
                    await db.StoreImage(id, articleImage.InputStream, articleImage.FileName);
                }

                return RedirectToAction("Index");
            }
            return View("CreateArticle",article);
        }


        [HttpPost]
        public async Task<ActionResult> AttachImage(string id, HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile != null)
            {
                await db.StoreImage(id, uploadedFile.InputStream, uploadedFile.FileName);
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> GetImage(string id)
        {
            var image = await db.GetImage(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return File(image, "image/png");
        }
    }
}