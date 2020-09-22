using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class Product
    {
        public Product()
        {
            ProductAttribute = new HashSet<ProductAttribute>();
            ProductQuantitySet = new HashSet<ProductQuantitySet>();
            ProductBlob = new HashSet<ProductBlob>();
            ProductReview = new HashSet<ProductReview>();
            ProductSpecification = new HashSet<ProductSpecification>();
            VendorProductCategoryList = new HashSet<VendorProductCategoryList>();
        }

        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public int CatalogId { get; set; }
        public int? BrandId { get; set; }
        public int? SeoId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string AdminComment { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public int? DisplayOrder { get; set; }
        public decimal OldPrice { get; set; }
        public decimal? SpecialPrice { get; set; }
        public DateTime? SpecialPriceStartDateTimeUtc { get; set; }
        public DateTime? SpecialPriceEndDateTimeUtc { get; set; }
        public bool Published { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual AllBrand Brand { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual Seo Seo { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttribute { get; set; }
        public virtual ICollection<ProductQuantitySet> ProductQuantitySet { get; set; }
        public virtual ICollection<ProductSpecification> ProductSpecification { get; set; }
        public virtual ICollection<ProductBlob> ProductBlob { get; set; }
        public virtual ICollection<ProductReview> ProductReview { get; set; }
        public virtual ICollection<VendorProductCategoryList> VendorProductCategoryList { get; set; }
    }
}
