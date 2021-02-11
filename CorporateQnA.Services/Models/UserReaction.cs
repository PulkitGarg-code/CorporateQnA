
using static CorporateQnA.Services.Models.Enum;

namespace CorporateQnA.Services.Models
{
    public class UserReaction
    {
        public int ReactionId { get; set; }

        public int AnswerId { get; set; }

        public string UserID { get; set; }

        public ReactionTypes Reaction { get; set; }

    }
}
