namespace ecommerce_api.Helpers.Params
{
    public class CustomerProductParam
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int categoryId { get; set; }
        public int subCategoryId { get; set; }
        public int subSubCategoryId { get; set; }
        public int brandId { get; set; }
    }
}