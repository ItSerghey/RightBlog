using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using RightBlog.Context;

namespace RightBlog.Controllers
{
    public class ContentController : Controller
    {
        private readonly ArticleContext db = new ArticleContext();

        public ActionResult Index()
        {
            return View("Index");
        }

        public async Task<ActionResult> GetArticles()
        {
            var articles = await db.GetArticles(null, null);
            //  var model = new ComputerList { Computers = computers, Filter = filter };

            return View("ContentPreview",articles);
        }
    }
}