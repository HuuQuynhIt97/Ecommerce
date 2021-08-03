using System;

namespace ecommerce_api.DTO
{
    public class HomeCategoryDto
    {
        public HomeCategoryDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int Category_ID { get; set; }
        public string Subsubcategories { get; set; }
        public int Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}