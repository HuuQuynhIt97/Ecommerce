using System;

namespace ecommerce_api.DTO
{
    public class ProductStockDto
    {
        public ProductStockDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public string Variant { get; set; }
        public string Sku { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public int image { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}