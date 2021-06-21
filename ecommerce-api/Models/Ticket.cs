using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Ticket
    {
        public Ticket()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Code { get; set; }
        public int User_ID { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public string Files { get; set; }
        public string Status { get; set; }
        public int Viewed { get; set; }
        public int Client_viewed { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}