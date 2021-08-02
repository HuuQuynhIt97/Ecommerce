using System;

namespace ecommerce_api.DTO
{
    public class Business_settingDto
    {
        public Business_settingDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}