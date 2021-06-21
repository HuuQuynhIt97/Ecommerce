using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Blog
    {
        public Blog()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Category_ID { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Short_description { get; set; }
        public string Description { get; set; }
        public int Banner { get; set; }
        public string Meta_title { get; set; }
        public string Meta_img { get; set; }
        public string Meta_description { get; set; }
        public string Meta_keywords { get; set; }
        public bool Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Deleted_at { get; set; }

    }
}