using System;

namespace ecommerce_api.DTO
{
    public class ReviewDto
    {
        public ReviewDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public int User_ID { get; set; }
        public int Rating { get; set; }
        public string comment { get; set; }
        public int Status { get; set; }
        public int Viewed { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}