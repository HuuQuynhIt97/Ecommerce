using System;

namespace ecommerce_api.DTO
{
    public class TaxeDto
    {
        public TaxeDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Tax_status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}