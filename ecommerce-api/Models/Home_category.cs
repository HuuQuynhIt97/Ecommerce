using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Home_category
    {
        public Home_category()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Category_ID { get; set; }
        public string Subsubcategories { get; set; }
        public int Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}