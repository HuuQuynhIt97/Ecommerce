using System;

namespace ecommerce_api.DTO
{
    public class SubscribeDto
    {
        public SubscribeDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Email { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}