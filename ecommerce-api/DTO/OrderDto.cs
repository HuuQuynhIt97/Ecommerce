using System;

namespace ecommerce_api.DTO
{
    public class OrderDto
    {
        public OrderDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int Guest_ID { get; set; }
        public int Seller_ID { get; set; }
        public string Shipping_address { get; set; }
        public string Delivery_status { get; set; }
        public string Payment_type { get; set; }
        public string Payment_status { get; set; }
        public string Payment_details { get; set; }
        public double Grand_total { get; set; }
        public double Coupon_discount { get; set; }
        public string Code { get; set; }
        public int Date { get; set; }
        public int Viewed { get; set; }
        public int Delivery_viewed { get; set; }
        public int Payment_status_viewed { get; set; }
        public int Commission_calculated { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}