using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Flash_deal
    {
        public Flash_deal()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public string Discount_type { get; set; }
        public bool Status { get; set; }
        public int Featured { get; set; }
        public string Background_color { get; set; }
        public string Text_color { get; set; }
        public string Banner { get; set; }
        public string Slug { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}