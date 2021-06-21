using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Flash_deal_product
    {
        public Flash_deal_product()
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