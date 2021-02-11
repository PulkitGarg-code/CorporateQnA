
using System.Data;
using Dapper;
using CorporateQnA.Services.Models;

namespace CorporateQnA.Services.Services
{
    public class BestAnswerService : IBestAnswerService
    {
        private IDbConnection DbConnection { get; set; }

        public BestAnswerService(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public void AddBestAnswerChoice(BestAnswer bestAnswer)
        {
           string query = @"INSERT INTO BestAnswers VALUES(@AnswerId,@UserID,@IsBestAnswer)";
           DbConnection.Execute(query, bestAnswer);
        }

        public void DeleteBestAnswerChoice(BestAnswer bestAnswer)
        {
            string query = @"DELETE FROM BestAnswers WHERE UserId=@userID AND AnswerId=@AnswerId";
            DbConnection.Execute(query, new { userID = bestAnswer.UserID, AnswerId = bestAnswer.AnswerId });
        }

    }
}
