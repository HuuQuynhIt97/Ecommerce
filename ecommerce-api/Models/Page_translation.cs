using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Page_translation
    {
        public Page_translation()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Page_ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Lang { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}