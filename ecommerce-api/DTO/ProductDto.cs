using System;

namespace ecommerce_api.DTO
{
    public class ProductDto
    {
        public ProductDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Added_by { get; set; }
        public int User_ID { get; set; }
        public int Category_ID { get; set; }
        public int Brand_ID { get; set; }
        public string Photos { get; set; }
        public string Thumbnail_img { get; set; }
        public string Video_provider { get; set; }
        public string Video_link { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public double Unit_price { get; set; }
        public double Purchase_price { get; set; }
        public int Variant_product { get; set; }
        public string Attributes { get; set; }
        public string Choice_options { get; set; }
        public string Colors { get; set; }
        public string Variations { get; set; }
        public int Todays_deal { get; set; }
        public int Published { get; set; }
        public string Stock_visibility_state { get; set; }
        public bool Cash_on_delivery { get; set; }
        public int Featured { get; set; }
        public int Seller_featured { get; set; }
        public int Current_stock { get; set; }
        public string Unit { get; set; }
        public int Min_qty { get; set; }
        public int Low_stock_quantity { get; set; }
        public double Discount { get; set; }
        public string Discount_type { get; set; }
        public double Tax { get; set; }
        public string Tax_type { get; set; }
        public string Shipping_type { get; set; }
        public string Shipping_code { get; set; }
        public bool Is_quantity_multiplied { get; set; }
        public int Est_shipping_days { get; set; }
        public int Num_of_sale { get; set; }
        public string Meta_title { get; set; }
        public string Meta_description { get; set; }
        public string Meta_img { get; set; }
        public string Pdf { get; set; }
        public string Slug { get; set; }
        public double Rating { get; set; }
        public string Barcode { get; set; }
        public int Digital { get; set; }
        public string File_name { get; set; }
        public string File_path { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}