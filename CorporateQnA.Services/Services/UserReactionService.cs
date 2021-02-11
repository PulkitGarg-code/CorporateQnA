using CorporateQnA.Services.Models;
using System.Data;
using Dapper;
using System.Linq;
using static CorporateQnA.Services.Models.Enum;

namespace CorporateQnA.Services.Services
{
    public class UserReactionService : IUserReactionService
    {
        private IDbConnection DbConnection { get; set; }

        public UserReactionService(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public void AddUserReaction(UserReaction userReaction)
        {
            string query = @"INSERT INTO UserReactions VALUES(@AnswerId,@UserID,@Reaction)";
            DbConnection.Execute(query, userReaction);
        }

        public void DeleteUserReaction(UserReaction userReaction)
        {
            string query = @"DELETE FROM UserReactions WHERE UserID=@userId AND AnswerId=@answerId";
            DbConnection.Execute(query, new { userId = userReaction.UserID, AnswerId = userReaction.AnswerId });
        }

        public bool checkUserHasAlreadyReacted(int answerId, string userId,int reaction)
        {
            string query = @"SELECT * FROM UserReactions WHERE AnswerId=@aid AND UserID=@uid AND reaction=@rea";
            int reactionsCount = DbConnection.Query<UpVote>(query, new { aid = answerId, uid = userId, rea=reaction }).Count();

            if (reactionsCount > 0)
                return true;
            return false;
        }

    }
}
