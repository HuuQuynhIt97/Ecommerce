using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Payment
    {
        public Payment()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Seller_ID { get; set; }
        public double Amount { get; set; }
        public string Payment_Details { get; set; }
        public string Payment_method { get; set; }
        public string Txn_code { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}