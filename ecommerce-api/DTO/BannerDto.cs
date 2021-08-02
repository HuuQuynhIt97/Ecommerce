using System;

namespace ecommerce_api.DTO
{
    public class BannerDto
    {
        public BannerDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Photo { get; set; }
        public string Url { get; set; }
        public int Position { get; set; }
        public bool Published { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}