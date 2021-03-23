using Paging.Infrastructure;
using System;
using System.Collections.Generic;

namespace eSuperShop.Repository
{






    public class ProductUnpublishedModel
    {
        public int ProductId { get; set; }
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string SlugUrl { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int StockQuantity { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }

    public class ProductListVendorCategoryWiseModel
    {
        public int ProductId { get; set; }
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string SlugUrl { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int StockQuantity { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public bool IsAssign { get; set; }
    }


    public class ProductQuantityAddModel
    {
        public ProductQuantityAddModel()
        {
            ProductQuantitySetAttribute = new HashSet<ProductQuantitySetAttributeModel>();
        }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAdjustment { get; set; }
        public virtual ICollection<ProductQuantitySetAttributeModel> ProductQuantitySetAttribute { get; set; }
    }

    public class ProductQuantitySetAttributeModel
    {
        public int ProductAttributeValueId { get; set; }
    }

    public class ProductQuantityCheckModel
    {
        public int ProductId { get; set; }
        public int[] ProductAttributeValueIds { get; set; }
    }

    public class ProductQuantitySetAddReturnModel
    {
        public int StockQuantity { get; set; }
        public ProductQuantitySetViewModel QuantitySet { get; set; }
    }
    public class ProductQuantitySetUpdateReturnModel
    {
        public int ProductQuantitySetId { get; set; }
        public int StockQuantity { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAdjustment { get; set; }
    }
    public class ProductQuantityViewModel
    {
        public int ProductQuantitySetId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAdjustment { get; set; }

    }




    public class ProductFilterRequest : PageRequest
    {
        public string SlugUrl { get; set; }
    }

    public class ProductListViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string SlugUrl { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public string ImageFileName { get; set; }
        public string StoreName { get; set; }
        public double Rating { get; set; }
        public int Sold { get; set; }
        public int RatingBy { get; set; }
    }

    public class ProductShortInfo
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string SlugUrl { get; set; }
        public string ImageFileName { get; set; }
    }

    public class ProductPendingApprovalListModel
    {
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string Email { get; set; }
        public string StoreName { get; set; }
        public string ProductName { get; set; }
        public string ProductSlugUrl { get; set; }
        public string ImageFileName { get; set; }
        public string ShortDescription { get; set; }
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }

    public class ProductApprovedModel
    {
        public int ProductId { get; set; }
        public int? BrandId { get; set; }
        public string Name { get; set; }
        public string SlugUrl { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string AdminComment { get; set; }
    }


    public class ProductBlobFileChangeModel
    {
        public int ProductId { get; set; }
        public string BlobFileName { get; set; }
    }
}

