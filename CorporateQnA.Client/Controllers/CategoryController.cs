using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CorporateQnA.Services.Models;
using CorporateQnA.Services.Services;
using CorporateQnA.Services.ViewModels;

namespace CorporateQnA.Client.Controllers
{
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService CategoryService;

        public CategoryController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        // api/Category/All
        [Route("All")]
        public IEnumerable<Category> GetAllCategories()
        {
            return CategoryService.GetAllCategories();
        }

        // api/Category/AllViews
        [Route("AllViews")]
        public IEnumerable<CategoryViewModel> GetAllCategoriesView()
        {
            return CategoryService.GetAllCategoriesView();
        }

        // api/Category/AllNames
        [Route("AllNames")]
        public IEnumerable<CategoryNameViewModel> GetAllCategoriesName()
        {
            return CategoryService.GetAllCategoriesName();
        }

        // api/Category/Add
        [Route("Add")]
        public void AddCategory([FromBody] Category category)
        {
            if (ModelState.IsValid)
                CategoryService.AddCategory(category);
        }

        // api/Category/:id/Delete
        [Route("{id}/Delete")]
        public void DeleteCategory(int id)
        {
            CategoryService.DeleteCategory(id);
        }
    }
}
