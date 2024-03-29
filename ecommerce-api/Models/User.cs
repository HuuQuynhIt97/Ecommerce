﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ecommerce_api.Models
{
    public class User
    {
        public User()
        {
            Created_at = DateTime.Now;
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
        public string AccessTokenLineNotify { get; set; }
        public byte[] ImageBase64 { get; set; }
        public int Referred_by { get; set; } 
        public string Provider_id { get; set; }
        public string User_type { get; set; }
        public string Verification_code { get; set; }
        public string New_email_verificiation_code { get; set; }
        public string Avatar { get; set; }
        public string Avatar_original { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Postal_code { get; set; }
        public string Phone { get; set; }
        public double Balance { get; set; }
        public bool Banned { get; set; }
        public string Referral_code { get; set; }
        public int Customer_package_id { get; set; }
        public int Remaining_uploads { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Email_verified_at { get; set; }
    }
}