
namespace CorporateQnA.Services.Models
{
    public class BestAnswer
    {
        public int BestAnswerId { get; set; }

        public int AnswerId { get; set; }

        public string UserID { get; set; }

        public bool? IsBestAnswer { get; set; }

    }

}
