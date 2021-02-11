using System;

namespace CorporateQnA.Services.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }

        public int QuestionId { get; set; }

        public string UserID { get; set; }

        public string AnswerContent { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
