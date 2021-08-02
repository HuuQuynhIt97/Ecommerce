using System;

namespace ecommerce_api.DTO
{
    public class ProductTaxeDto
    {
        public ProductTaxeDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public int Tax_ID { get; set; }
        public double Tax { get; set; }
        public string Tax_type { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}