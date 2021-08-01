using System;

namespace ecommerce_api.DTO
{
    public class CityDto
    {
        public CityDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Country_ID { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}