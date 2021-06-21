using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Banner
    {
        public Banner()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Photo { get; set; }
        public string Url { get; set; }
        public int Position { get; set; }
        public bool Published { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}