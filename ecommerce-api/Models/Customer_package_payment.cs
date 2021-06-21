using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Customer_package_payment
    {
        public Customer_package_payment()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int Customer_package_ID { get; set; }
        public string Payment_method { get; set; }
        public string Payment_detail { get; set; }
        public int Approval { get; set; }
        public int Offline_payment { get; set; }
        public string Reciept { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}