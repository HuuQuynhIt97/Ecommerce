using System;

namespace ecommerce_api.DTO
{
    public class CustomerPackageDto
    {
        public CustomerPackageDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public int Product_upload { get; set; }
        public string Logo { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}