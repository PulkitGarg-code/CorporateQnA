using CorporateQnA.Services.Models;
using System.Data;
using Dapper;
using System.Linq;

namespace CorporateQnA.Services.Services
{
	public class UpVoteService : IUpVoteService
	{
		private IDbConnection DbConnection { get; set; }

		public UpVoteService(IDbConnection dbConnection)
		{
			DbConnection = dbConnection;
		}

		public void AddUpVoteOnQuestion(UpVote upVote)
		{
			string query = @"INSERT INTO UpVotes VALUES(@QuestionId,@UserID,@CreatedAt)";
			DbConnection.Execute(query, upVote);
		}

		public bool CheckUpVoteByUserOnQuestion(int questionId, string userId)
		{
			string query = @"SELECT * FROM UpVotes WHERE QuestionId=@qid AND UserID=@uid";
			int upVotesCount = DbConnection.Query<UpVote>(query, new { qid = questionId, uid = userId }).Count();

			if (upVotesCount > 0)
				return true;
			return false;
		}

		public void DeleteUpVoteOnQuestionId(int questionId)
		{
			string query = @"DELETE FROM UpVotes WHERE QuestionId=@id";
			DbConnection.Execute(query, new { Id = questionId });
		}

	}
}
