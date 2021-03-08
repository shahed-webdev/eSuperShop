using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class Vendor
    {
        public Vendor()
        {
            Product = new HashSet<Product>();
            VendorCatalog = new HashSet<VendorCatalog>();
            VendorFollower = new HashSet<VendorFollower>();
            VendorReview = new HashSet<VendorReview>();
            VendorWarehouse = new HashSet<VendorWarehouse>();
            VendorStoreSlider = new HashSet<VendorStoreSlider>();
            VendorProductCategory = new HashSet<VendorProductCategory>();
            VendorCertificate = new HashSet<VendorCertificate>();
        }

        public int VendorId { get; set; }
        public int? RegistrationId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string Email { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StoreSlugUrl { get; set; }
        public string StoreBannerUrl { get; set; }
        public string StoreLogoUrl { get; set; }
        public string StoreTagLine { get; set; }
        public StoreTheme StoreTheme { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedByRegistrationId { get; set; }
        public DateTime? ApprovedOnUtc { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public decimal GrossSale { get; set; }
        public decimal Discount { get; set; }
        public decimal Refund { get; set; }
        public decimal NetSale { get; set; }
        public decimal Commission { get; set; }
        public decimal Withdraw { get; set; }
        public decimal Balance { get; set; }

        public string StorePostcode { get; set; }
        public int? StoreAreaId { get; set; }
        public Area StoreArea { get; set; }
        public string NId { get; set; }
        public string NIdImageFrontUrl { get; set; }
        public string NIdImageBackUrl { get; set; }
        public string TradeLicenseImageUrl { get; set; }
        public string BankAccountTitle { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string RoutingNumber { get; set; }
        public string ChequeImageUrl { get; set; }
        public string MobileBankingType { get; set; }
        public string MobileBankingNumber { get; set; }
        public string WarehouseAddress { get; set; }
        public string WarehousePhone { get; set; }
        public string WarehousePostcode { get; set; }
        public int? WarehouseAreaId { get; set; }
        public Area WarehouseArea { get; set; }
        public string ReturnAddress { get; set; }
        public string ReturnPhone { get; set; }
        public string ReturnPostcode { get; set; }
        public int? ReturnAreaId { get; set; }
        public Area ReturnArea { get; set; }
        public virtual ICollection<VendorCertificate> VendorCertificate { get; set; }


        public virtual Registration Registration { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<VendorCatalog> VendorCatalog { get; set; }
        public virtual ICollection<VendorFollower> VendorFollower { get; set; }
        public virtual ICollection<VendorReview> VendorReview { get; set; }
        public virtual ICollection<VendorWarehouse> VendorWarehouse { get; set; }
        public virtual ICollection<VendorStoreSlider> VendorStoreSlider { get; set; }
        public virtual ICollection<VendorProductCategory> VendorProductCategory { get; set; }




    }
}
