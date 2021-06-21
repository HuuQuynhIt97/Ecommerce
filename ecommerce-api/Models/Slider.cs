using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Slider
    {
        public Slider()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Photo { get; set; }
        public int Published { get; set; }
        public string Link { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}