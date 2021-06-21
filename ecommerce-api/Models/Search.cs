using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Search
    {
        public Search()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Query { get; set; }
        public int  Count { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}