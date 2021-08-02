using System;

namespace ecommerce_api.DTO
{
    public class CountryDto
    {
        public CountryDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Code  { get; set; }
        public string Name  { get; set; }
        public bool Status  { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}