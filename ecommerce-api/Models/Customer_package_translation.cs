using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Customer_package_translation
    {
        public Customer_package_translation()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Customer_package_ID { get; set; }
        public string Name { get; set; }
        public string Lang { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}