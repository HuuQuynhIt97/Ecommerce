using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Ticket_reply
    {
        public Ticket_reply()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Ticket_ID { get; set; }
        public int User_ID { get; set; }
        public string Reply { get; set; }
        public string Files { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}