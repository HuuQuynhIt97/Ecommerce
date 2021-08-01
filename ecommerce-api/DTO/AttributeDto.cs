using System;

namespace ecommerce_api.DTO
{
    public class AttributeDto
    {
        public AttributeDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}