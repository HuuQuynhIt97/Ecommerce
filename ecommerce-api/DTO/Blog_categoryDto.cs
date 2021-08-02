using System;

namespace ecommerce_api.DTO
{
    public class Blog_categoryDto
    {
        public Blog_categoryDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Category_name { get; set; }
        public string Slug { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Deleted_at { get; set; }
    }
}