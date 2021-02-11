using CorporateQnA.Services.Models;
using System.Collections.Generic;
using System.Data;
using Dapper;
using CorporateQnA.Services.ViewModels;
using System.Linq;

namespace CorporateQnA.Services.Services
{
	public class QuestionService : IQuestionService
	{
		private IDbConnection DbConnection { get; set; }

		public QuestionService(IDbConnection dbConnection)
		{
			DbConnection = dbConnection;
			dbConnection.Open();
		}

		public IEnumerable<Question> GetAllQuestions()
		{
			string query = @"SELECT * FROM Questions";
			return DbConnection.Query<Question>(query);
		}

		public IEnumerable<QuestionsViewModel> GetAllQuestionsViewModel()
		{
			string query = @"SELECT * FROM QuestionsViewModel";
			return DbConnection.Query<QuestionsViewModel>(query);
		}

		public QuestionsViewModel GetQuestionViewModel(int id)
		{
			string query = @"SELECT * FROM QuestionsViewModel WHERE questionId=@id";
			return DbConnection.Query<QuestionsViewModel>(query, new { id = id }).First();
		}

		public void IncrementTotalViewsCount(int id)
		{
			string query = @"UPDATE Questions SET ViewsCount=ViewsCount+1 WHERE QuestionId=@id";
			DbConnection.Execute(query, new { Id = id });
		}

		public void AddQuestion(Question question)
		{
			string query = @"INSERT INTO Questions VALUES(@UserID,@CategoryId,@Title,@Description,@ViewsCount,@Resolved,@CreatedAt)";
			DbConnection.Execute(query, question);
		}

		public void DeleteQuestion(int id)
		{
			string query = @"DELETE FROM Questions WHERE QuestionId=@id";
			DbConnection.Execute(query, new { Id = id });
		}

	}
}
