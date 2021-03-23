using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public class ProductDetailsViewModel
    {
        public ProductDetailsViewModel()
        {
            Specifications = new HashSet<SpecificationFilterModel>();
            Reviews = new HashSet<ProductReviewViewModel>();
            Attributes = new HashSet<ProductAttributeViewModel>();
            Faqs = new HashSet<FaqProductWiseViewModel>();
            RecommendedProducts = new HashSet<ProductListViewModel>();
            VendorMoreProducts = new HashSet<ProductListViewModel>();
        }
        public int ProductId { get; set; }
        public int CatalogId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string StoreName { get; set; }
        public string StoreSlugUrl { get; set; }
        public string FullDescription { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int StockQuantity { get; set; }
        public int Sold { get; set; }
        public ProductReviewAverageModel AverageReview { get; set; }
        public CatalogHierarchyModel CatalogBreadcrumb { get; set; }
        public string[] Blobs { get; set; }
        public ICollection<SpecificationFilterModel> Specifications { get; set; }
        public ICollection<ProductReviewViewModel> Reviews { get; set; }
        public ICollection<ProductAttributeViewModel> Attributes { get; set; }
        public int TotalFaqs { get; set; }
        public ICollection<FaqProductWiseViewModel> Faqs { get; set; }
        public ICollection<ProductListViewModel> RecommendedProducts { get; set; }
        public ICollection<ProductListViewModel> VendorMoreProducts { get; set; }

    }
}