
using System.Collections.Generic;
using System.Data;
using Dapper;
using CorporateQnA.Services.ViewModels;
using CorporateQnA.Services.Models;
using System.Linq;

namespace CorporateQnA.Services.Services
{
	public class UserService : IUserService
	{
		private IDbConnection DbConnection { get; set; }

		public UserService(IDbConnection dbConnection)
		{
			DbConnection = dbConnection;
		}

		public IEnumerable<UsersViewModel> GetAllUsersViewModel()
		{
			string query = @"SELECT * FROM UsersViewModel";
			return DbConnection.Query<UsersViewModel>(query);
		}

		public AppUser GetUser(int userid)
		{
			string query = @"SELECT * FROM Users WHERE Id = @id";
			var user = this.DbConnection.Query<AppUser>(query, new { id = userid }).FirstOrDefault();
			return user;
		}

		public int Create(AppUser appUser)
		{
			string query = @"SELECT * FROM Users WHERE Email=@email";
			var check = DbConnection.Query(query, new { email = appUser.Email }).FirstOrDefault();

			if (check == null)
			{
				string query1 = @"INSERT INTO Users output INSERTED.Id VALUES(@FullName,@Email,@Location,@Designation,@Department,@ProfileImage)";
				var id = DbConnection.QuerySingle<int>(query1, new { FullName = appUser.FullName, Email = appUser.Email, Location = appUser.Location, Designation = appUser.Designation, Department = appUser.Department, ProfileImage = appUser.ProfileImage });
				return id;
			}
			else
			{
				return 0;
			}

		}

	}
}
