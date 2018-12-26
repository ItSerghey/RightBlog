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
        private readonly CategoryContext _categoryContext;

        public PreviewArticlesViewComponent(ArticleContext articleContext, CategoryContext categoryContext)
        {
            _articleContext = articleContext;
            _categoryContext = categoryContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _articleContext.GetArticles(true, null, null);
            var categories = await _categoryContext.GetCategories(true);
            //TODO make a badge great again
            articles.ForEach(c =>  c.Badge =  categories.Exists(a => a.Id == c.CategoryId) ?  categories.FirstOrDefault(a => a.Id == c.CategoryId) : null);

            return View("~/Views/Components/ContentPreview.cshtml", articles); 
        }
    }
}
