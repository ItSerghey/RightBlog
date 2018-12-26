using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RightBlog.Core.Data;


namespace RightBlog.Core.Components
{
    public class DropDownCategoryViewComponent : ViewComponent
    {
        private readonly CategoryContext _categoryContext;

        public DropDownCategoryViewComponent(CategoryContext categoryContext)
        {
            _categoryContext = categoryContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string selectedCategory)
        {

          
            var category = await _categoryContext.GetCategories(null);
            category.ForEach(c => c.Selected = false);

            if (!string.IsNullOrEmpty(selectedCategory))
                category.Find(c => c.Id == selectedCategory).Selected = true;

            return View("~/Views/Components/_CategoriesDropDownPartial.cshtml", category);
        }
    }
}
