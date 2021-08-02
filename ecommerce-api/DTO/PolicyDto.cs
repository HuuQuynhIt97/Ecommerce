using System;

namespace ecommerce_api.DTO
{
    public class PolicyDto
    {
        public PolicyDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}