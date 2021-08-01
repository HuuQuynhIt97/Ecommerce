using System;

namespace ecommerce_api.DTO
{
    public class ShopDto
    {
        public ShopDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Sliders { get; set; }
        public string Address { get; set; }
        public string facebook { get; set; }
        public string Google { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
        public string Slugs { get; set; }
        public string Meta_title { get; set; }
        public string Meta_Description { get; set; }
        public string Pick_up_point_id { get; set; }
        public double Shipping_cost { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}