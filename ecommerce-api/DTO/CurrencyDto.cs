using System;

namespace ecommerce_api.DTO
{
    public class CurrencyDto
    {
        public CurrencyDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Exchange_rate { get; set; }
        public bool Status { get; set; }
        public string Code { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}