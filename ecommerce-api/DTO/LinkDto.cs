using System;

namespace ecommerce_api.DTO
{
    public class LinkDto
    {
        public LinkDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}