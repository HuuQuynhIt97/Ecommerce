using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Pickup_point_translation
    {
        public Pickup_point_translation()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Pickup_point_ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Lang { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}