using Microsoft.AspNetCore.Mvc;
using RightBlog.Core.Data;
using RightBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RightBlog.Core.Components
{
    public class PreviewArticlesViewComponent: ViewComponent
    {
        private readonly ArticleContext _articleContext;

        public PreviewArticlesViewComponent(ArticleContext articleContext)
        {
            _articleContext = articleContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _articleContext.GetArticles(null, null);
            return View("~/Views/Components/ContentPreview.cshtml", articles); 
        }
    }
}
