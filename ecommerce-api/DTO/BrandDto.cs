using System;

namespace ecommerce_api.DTO
{
    public class BrandDto
    {
        public BrandDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public int Top { get; set; }
        public string Slug { get; set; }
        public string Meta_title { get; set; }
        public string Meta_description { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}