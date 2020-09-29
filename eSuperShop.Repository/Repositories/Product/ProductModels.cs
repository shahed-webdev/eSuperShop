using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class ProductAddModel
    {
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public int CatalogId { get; set; }
        public int? BrandId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
    }

    public class ProductAttributeAddModel
    {
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public ProductAttributeDisplay DisplayType { get; set; }


    }

    public class ProductAttributeValueAddModel
    {
        public int ProductAttributeId { get; set; }
        public string Value { get; set; }
        public string ImageUrl { get; set; }
    }
}