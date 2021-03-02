using eSuperShop.Data;
using Paging.Infrastructure;
using System;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public class VendorAddModel
    {
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string StoreAddress { get; set; }
        public string StorePostcode { get; set; }
        public int StoreAreaId { get; set; }
    }
    public class VendorModel
    {
        public int VendorId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string StoreRegion { get; set; }
        public string StoreArea { get; set; }
        public string StoreAddress { get; set; }
        public string StorePostcode { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
    public class VendorCatalogAssignModel
    {
        public VendorCatalogAssignModel()
        {
            Catalogs = new HashSet<VendorCatalogModel>();
        }
        public int VendorId { get; set; }
        public int AssignedByRegistrationId { get; set; }
        public ICollection<VendorCatalogModel> Catalogs { get; set; }
    }

    public class VendorCatalogModel
    {
        public int CatalogId { get; set; }
        public decimal CommissionPercentage { get; set; }
    }

    public class VendorCatalogViewModel
    {
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public decimal CommissionPercentage { get; set; }
    }
    public class VendorApprovedModel
    {
        public int VendorId { get; set; }
        public int ApprovedByRegistrationId { get; set; }
    }

    public class VendorInfoModel
    {
        public int VendorId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string StoreAddress { get; set; }
        public bool IsApproved { get; set; }
        public decimal GrossSale { get; set; }
        public decimal Discount { get; set; }
        public decimal Refund { get; set; }
        public decimal NetSale { get; set; }
        public decimal Commission { get; set; }
        public decimal Withdraw { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public StoreTheme StoreTheme { get; set; }
        public string StorePostcode { get; set; }
        public int StoreAreaId { get; set; }
        public string StoreRegion { get; set; }
        public string StoreArea { get; set; }
        public string NId { get; set; }
        public string NIdImageFrontUrl { get; set; }
        public string NIdImageBackUrl { get; set; }
        public string TradeLicenseImageUrl { get; set; }
        public string BankAccountTitle { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string RouthingNumber { get; set; }
        public string ChecqueImageUrl { get; set; }
        public string MobileBankingType { get; set; }
        public string MobileBankingNumber { get; set; }
        public string WarehouseAddress { get; set; }
        public string WarehousePostcode { get; set; }
        public int WarehouseAreaId { get; set; }
        public string WarehouseRegion { get; set; }
        public string WarehouseArea { get; set; }
        public string ReturnAddress { get; set; }
        public string ReturnPostcode { get; set; }
        public int ReturnAreaId { get; set; }
        public string ReturnRegion { get; set; }
        public string ReturnArea { get; set; }
    }

    public class VendorDashboardModel
    {
        public VendorDashboardModel()
        {
            Catalogs = new HashSet<VendorCatalogViewModel>();
        }

        public VendorInfoModel VendorInfo { get; set; }
        public ICollection<VendorCatalogViewModel> Catalogs { get; set; }
    }

    public class VendorStoreInfoUpdateModel
    {
        public int VendorId { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StoreSlugUrl { get; set; }
        public string StoreBannerUrl { get; set; }
        public string StoreLogoUrl { get; set; }
        public string StoreTagLine { get; set; }
    }

    public class StoreFilterRequest : PageRequest
    {
    }

    public class StoreProductFilterRequest : PageRequest
    {
        public int VendorId { get; set; }
    }
    public class StoreViewModel
    {
        public int VendorId { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StoreSlugUrl { get; set; }
        public string StoreLogoUrl { get; set; }
        public string StoreTagLine { get; set; }
        public double Rating { get; set; }
        public int RatingBy { get; set; }

        public string[] ProductImageUrls { get; set; }
    }

    public class StoreThemeViewModel
    {
        public StoreThemeViewModel()
        {
            VendorStoreSlider = new HashSet<VendorStoreSliderViewModel>();
        }
        public int VendorId { get; set; }
        public string StoreName { get; set; }
        public string StoreSlugUrl { get; set; }
        public StoreTheme StoreTheme { get; set; }
        public string StoreBannerUrl { get; set; }
        public string StoreLogoUrl { get; set; }
        public string StoreTagLine { get; set; }
        public double Rating { get; set; }
        public int RatingBy { get; set; }
        public ICollection<VendorStoreSliderViewModel> VendorStoreSlider { get; set; }
    }

    public class VendorStoreSliderViewModel
    {
        public string ImageUrl { get; set; }
        public string RedirectUrl { get; set; }
    }

    public class StoreProductViewModel
    {
        public StoreProductViewModel()
        {
            Products = new HashSet<ProductListViewModel>();
        }
        public int VendorProductCategoryId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<ProductListViewModel> Products { get; set; }
    }
}