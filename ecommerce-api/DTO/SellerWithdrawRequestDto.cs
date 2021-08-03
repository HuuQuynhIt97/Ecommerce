using System;

namespace ecommerce_api.DTO
{
    public class SellerWithdrawRequestDto
    {
        public SellerWithdrawRequestDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int User_ID { get; set; }
        public double Amount { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public int Viewed { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}