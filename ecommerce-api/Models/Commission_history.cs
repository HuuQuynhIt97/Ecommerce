using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Commission_history
    {
        public Commission_history()
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