using System;

namespace ecommerce_api.DTO
{
    public class SeoSettingDto
    {
        public SeoSettingDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Keyword { get; set; }
        public int Author { get; set; }
        public int Revisit { get; set; }
        public string Sitemap_link { get; set; }
        public string Description { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}