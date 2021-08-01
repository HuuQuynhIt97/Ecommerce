using System;

namespace ecommerce_api.DTO
{
    public class CouponDto
    {
        public CouponDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Details { get; set; }
        public double Discount { get; set; }
        public string Discount_type { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}