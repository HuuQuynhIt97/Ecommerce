using System;

namespace ecommerce_api.DTO
{
    public class CategoryDto
    {
        public CategoryDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Parent_ID { get; set; }
        public int  Level { get; set; }
        public string Name { get; set; }
        public int Order_level { get; set; }
        public double Commission_rate { get; set; }
        public string Banner { get; set; }
        public string Icon { get; set; }
        public int Featured { get; set; }
        public int Top { get; set; }
        public int Digital { get; set; }
        public string Slug { get; set; }
        public string Meta_title { get; set; }
        public string Meta_description { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}