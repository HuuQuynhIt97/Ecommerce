using System;

namespace ecommerce_api.DTO
{
    public class WalletDto
    {
        public WalletDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int User_ID { get; set; }
        public double Amount { get; set; }
        public string Payment_method { get; set; }
        public string Payment_details { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}