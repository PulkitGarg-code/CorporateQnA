using CorporateQnA.Services.Models;
using System.Data;
using Dapper;
using System.Linq;

namespace CorporateQnA.Services.Services
{
	public class ReportQuestionService : IReportQuestionService
	{
		private IDbConnection DbConnection { get; set; }

		public ReportQuestionService(IDbConnection dbConnection)
		{
			DbConnection = dbConnection;
		}

		public void AddReportRequestForQuestion(ReportQuestion reportQuestion)
		{
			string query = @"INSERT INTO ReportedQuestions VALUES(@QuestionId,@UserID,@ReportedAt)";
			DbConnection.Execute(query, reportQuestion);
		}

		public bool CheckQuestionReportedByUser(int questionId, string userId)
		{
			string query = @"SELECT * FROM ReportedQuestions WHERE QuestionId=@qid AND UserID=@uid";
			int reportedQuestionsCount = DbConnection.Query<ReportQuestion>(query, new { qid = questionId, uid = userId }).Count();

			if (reportedQuestionsCount > 0)
				return true;
			return false;
		}

		public int GetCountOfReportedQuestion(ReportQuestion reportQuestion)
		{
			string query = @"SELECT * FROM ReportedQuestions WHERE questionId=@qid AND userID=@uid";
			return DbConnection.Query(query, new { qid = reportQuestion.QuestionId, uid = reportQuestion.UserId }).Count();
			
		}

		public void DeleteReportRequestForQuestion(ReportQuestion reportQuestion)
		{
			string query = @"DELETE FROM ReportedQuestions WHERE UserId=@userID AND QuestionId=@questionId";
			DbConnection.Execute(query, new { userID = reportQuestion.UserId, QuestionId = reportQuestion.QuestionId });
		}

	}
}
