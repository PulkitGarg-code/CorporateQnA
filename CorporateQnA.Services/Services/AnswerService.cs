
using System.Collections.Generic;
using System.Data;
using Dapper;
using CorporateQnA.Services.ViewModels;
using CorporateQnA.Services.Models;

namespace CorporateQnA.Services.Services
{
    public class AnswerService: IAnswerService
    {
        private IDbConnection DbConnection { get; set; }

        public AnswerService(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public IEnumerable<AnswersViewModel> GetAllAnswersViewModel(int questionId)
        {
            string query = @"EXEC SelectAllAnswersViewModel @QuestionId = @id;";
           return DbConnection.Query<AnswersViewModel>(query, new { Id = questionId });

        }

        public void AddAnswer(Answer answer)
        {
            string query = @"INSERT INTO Answers values(@QuestionId,@UserID,@AnswerContent,@CreatedAt)";
            DbConnection.Execute(query, answer);
        }

        public void DeleteAnswer(int id)
        {
            string query = @"DELETE FROM Answers WHERE AnswerId=@id";
            DbConnection.Execute(query, new { Id = id });
        }

    }
}
