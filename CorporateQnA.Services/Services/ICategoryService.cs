using CorporateQnA.Services.Models;
using CorporateQnA.Services.ViewModels;
using System.Collections.Generic;


namespace CorporateQnA.Services.Services
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetAllCategories();
        public IEnumerable<CategoryViewModel> GetAllCategoriesView();
        public IEnumerable<CategoryNameViewModel> GetAllCategoriesName();
        public void AddCategory(Category category);
        public void DeleteCategory(int id);
    }
}
