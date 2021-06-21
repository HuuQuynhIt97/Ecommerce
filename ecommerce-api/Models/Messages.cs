using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Messages
    {
        public Messages()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Conversation_ID { get; set; }
        public int User_ID { get; set; }
        public string Message { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}