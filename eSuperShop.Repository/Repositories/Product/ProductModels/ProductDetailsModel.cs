using eSuperShop.Data;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public class ProductDetailsModel
    {
        public ProductDetailsModel()
        {
            Attributes = new HashSet<ProductAttributeViewModel>();
            Blobs = new HashSet<ProductBlobViewModel>();
            Specifications = new HashSet<ProductSpecificationViewModel>();
            QuantitySets = new HashSet<ProductQuantitySetViewModel>();
        }
        public int ProductId { get; set; }
        public VendorInfoModel VendorInfo { get; set; }
        public CatalogDisplayModel CatalogInfo { get; set; }
        public BrandModel BrandInfo { get; set; }
        public string Name { get; set; }
        public string SlugUrl { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool Published { get; set; }

        public ICollection<ProductQuantitySetViewModel> QuantitySets { get; set; }
        public ICollection<ProductAttributeViewModel> Attributes { get; set; }
        public ICollection<ProductBlobViewModel> Blobs { get; set; }
        public ICollection<ProductSpecificationViewModel> Specifications { get; set; }

    }


    public class ProductQuantitySetViewModel
    {
        public ProductQuantitySetViewModel()
        {
            AttributesWithValue = new HashSet<ProductQuantitySetAttributeViewModel>();
        }
        public int ProductQuantitySetId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAdjustment { get; set; }
        public virtual ICollection<ProductQuantitySetAttributeViewModel> AttributesWithValue { get; set; }
    }

    public class ProductQuantitySetAttributeViewModel
    {
        public int ProductQuantitySetAttributeId { get; set; }
        public int AttributeId { get; set; }
        public string KeyName { get; set; }
        public int ProductAttributeValueId { get; set; }
        public string Value { get; set; }
    }

    public class ProductAttributeViewModel
    {
        public ProductAttributeViewModel()
        {
            Values = new HashSet<ProductAttributeValueViewModel>();
        }
        public int ProductAttributeId { get; set; }
        public int AttributeId { get; set; }
        public int ProductId { get; set; }
        public string KeyName { get; set; }
        public ProductAttributeDisplay DisplayType { get; set; }
        public ICollection<ProductAttributeValueViewModel> Values { get; set; }
    }
    public class ProductBlobViewModel
    {
        public int ProductBlobId { get; set; }
        public int ProductId { get; set; }
        public string BlobFileName { get; set; }
        public int? DisplayOrder { get; set; }
    }
    public class ProductAttributeValueViewModel
    {
        public int ProductAttributeValueId { get; set; }
        public int ProductAttributeId { get; set; }
        public string Value { get; set; }
        public string ImageFileName { get; set; }
    }

    public class ProductSpecificationViewModel
    {
        public int ProductSpecificationId { get; set; }
        public int ProductId { get; set; }
        public int SpecificationId { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
    }
}