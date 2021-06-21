using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Country
    {
        public Country()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Code  { get; set; }
        public string Name  { get; set; }
        public bool Status  { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}