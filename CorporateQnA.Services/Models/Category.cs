using System;

namespace CorporateQnA.Services.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string UserID { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
