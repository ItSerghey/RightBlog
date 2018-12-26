using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RightBlog.Core.Data;

namespace RightBlog.Core.Components
{
    public class MenuCategoriesViewComponent : ViewComponent
    {
        private readonly CategoryContext _categoryContext;

        public MenuCategoriesViewComponent(CategoryContext categoryContext)
        {
            _categoryContext = categoryContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = await _categoryContext.GetCategories(true);
            return View("~/Views/Components/_CategoriesMenuPartial.cshtml", category);
        }
    }
}
