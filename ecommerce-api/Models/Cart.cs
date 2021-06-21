using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Cart
    {
        public Cart()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Owner_ID { get; set; }
        public int User_ID { get; set; }
        public string Temp_user_id { get; set; }
        public int Address_id { get; set; }
        public int Product_id { get; set; }
        public string Variation { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        public double Shipping_cost { get; set; }
        public string Shipping_type { get; set; }
        public int Pickup_point { get; set; }
        public double Discount { get; set; }
        public string Coupon_code { get; set; }
        public bool Coupon_applied { get; set; }
        public int Quantity { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}