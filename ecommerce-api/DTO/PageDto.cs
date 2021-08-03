using System;

namespace ecommerce_api.DTO
{
    public class PageDto
    {
        public PageDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string Meta_title { get; set; }
        public string Meta_description { get; set; }
        public string Keywords { get; set; }
        public string Meta_image { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}