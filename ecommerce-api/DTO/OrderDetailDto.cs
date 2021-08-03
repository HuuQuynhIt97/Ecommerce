using System;

namespace ecommerce_api.DTO
{
    public class OrderDetailDto
    {
        public OrderDetailDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Order_ID { get; set; }
        public int Seller_ID { get; set; }
        public int Product_ID { get; set; }
        public string Variation { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        public double Shipping_cost { get; set; }
        public int Quantity { get; set; }
        public string Payment_status { get; set; }
        public string Delivery_status { get; set; }
        public string Shipping_type { get; set; }
        public int Pickup_point_id { get; set; }

        public string Product_referral_code { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}