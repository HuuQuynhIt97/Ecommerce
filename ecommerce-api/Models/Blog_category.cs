using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Blog_category
    {
        public Blog_category()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Category_name { get; set; }
        public string Slug { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Deleted_at { get; set; }

    }
}