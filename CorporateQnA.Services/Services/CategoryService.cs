
using System.Collections.Generic;
using System.Data;
using Dapper;
using CorporateQnA.Services.ViewModels;
using CorporateQnA.Services.Models;

namespace CorporateQnA.Services.Services
{
	public class CategoryService : ICategoryService
	{
		private IDbConnection DbConnection { get; set; }

		public CategoryService(IDbConnection dbConnection)
		{
			DbConnection = dbConnection;
		}

		public IEnumerable<Category> GetAllCategories()
		{
			string query = @"SELECT * FROM Categories";
			return DbConnection.Query<Category>(query);
		}

		public IEnumerable<CategoryViewModel> GetAllCategoriesView()
		{
			string query = @"SELECT * FROM CategoryView";
			return DbConnection.Query<CategoryViewModel>(query);
		}

		public IEnumerable<CategoryNameViewModel> GetAllCategoriesName()
		{
			string query = @"SELECT * FROM CategoryNameViewModel";
			return DbConnection.Query<CategoryNameViewModel>(query);
		}

		public void AddCategory(Category category)
		{
			string query = @"INSERT INTO Categories VALUES(@UserID,@CategoryName,@Description,@CreatedAt)";
			DbConnection.Execute(query, category);
		}

		public void DeleteCategory(int id)
		{
			string query = @"DELETE FROM Categories WHERE CategoryId=@id";
			DbConnection.Execute(query, new { Id = id });
		}

	}
}
