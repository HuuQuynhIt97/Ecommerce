using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Seo_setting
    {
        public Seo_setting()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Keyword { get; set; }
        public int Author { get; set; }
        public int Revisit { get; set; }
        public string Sitemap_link { get; set; }
        public string Description { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}