using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class Upload
    {
        public Upload()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string File_original_name { get; set; }
        public string File_name { get; set; }
        public int User_ID { get; set; }
        public int File_size { get; set; }
        public string Extension { get; set; }
        public string Type { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Deleted_at { get; set; }

    }
}