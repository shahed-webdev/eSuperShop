using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public class ProductDetailsForSellerModel
    {
        public ProductDetailsForSellerModel()
        {
            Specifications = new List<ProductSpecificationForSellerModel>();
            QuantitySets = new List<ProductQuantitySetSellerModel>();
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string SlugUrl { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool Published { get; set; }
        public string[] BlobFileNames { get; set; }
        public string CatalogName { get; set; }
        public string BrandName { get; set; }
        public List<ProductSpecificationForSellerModel> Specifications { get; set; }
        public List<ProductQuantitySetSellerModel> QuantitySets { get; set; }
    }

    public class ProductQuantitySetSellerModel
    {
        public ProductQuantitySetSellerModel()
        {
            Values = new List<ProductQuantitySetValueSellerModel>();
        }
        public int Quantity { get; set; }
        public decimal PriceAdjustment { get; set; }
        public ICollection<ProductQuantitySetValueSellerModel> Values { get; set; }
    }

    public class ProductQuantitySetValueSellerModel
    {
        public string KeyName { get; set; }
        public string Value { get; set; }
    }

    public class ProductSpecificationForSellerModel
    {
        public string KeyName { get; set; }
        public string Value { get; set; }
    }



}