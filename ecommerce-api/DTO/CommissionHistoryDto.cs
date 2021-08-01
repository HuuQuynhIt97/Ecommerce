using System;

namespace ecommerce_api.DTO
{
    public class CommissionHistoryDto
    {
        public CommissionHistoryDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Order_ID { get; set; }
        public int Order_detail_ID { get; set; }
        public int Seller_ID { get; set; }
        public double Admin_commission { get; set; }
        public double Sell_earning { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}