using System;

namespace ecommerce_api.DTO
{
    public class SliderDto
    {
        public SliderDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Photo { get; set; }
        public int Published { get; set; }
        public string Link { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}