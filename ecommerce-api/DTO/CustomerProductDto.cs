using System;

namespace ecommerce_api.DTO
{
    public class CustomerProductDto
    {
        public CustomerProductDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Published { get; set; }
        public bool Status { get; set; }
        public string Added_by { get; set; }
        public int User_ID { get; set; }
        public int Category_ID { get; set; }
        public int Subcategory_ID { get; set; }
        public int Subsubcategory_ID { get; set; }
        public int Brand_ID { get; set; }
        public string Photos { get; set; }
        public string Thumbnail_img { get; set; }
        public string Conditon { get; set; }
        public string Location { get; set; }
        public string Video_provider { get; set; }
        public string Video_link { get; set; }
        public string Unit { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public double Unit_price { get; set; }
        public string Meta_title { get; set; }
        public string Meta_description { get; set; }
        public string Meta_img { get; set; }
        public string Pdf { get; set; }
        public string Slug { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}