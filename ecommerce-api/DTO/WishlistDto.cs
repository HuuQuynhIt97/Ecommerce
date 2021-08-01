using System;

namespace ecommerce_api.DTO
{
    public class WishlistDto
    {
        public WishlistDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int Product_ID { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}