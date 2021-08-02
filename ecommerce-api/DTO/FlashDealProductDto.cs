using System;

namespace ecommerce_api.DTO
{
    public class FlashDealProductDto
    {
        public FlashDealProductDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Flash_deal_ID { get; set; }
        public int Products_ID { get; set; }
        public double Discount { get; set; }
        public string Discount_type { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}