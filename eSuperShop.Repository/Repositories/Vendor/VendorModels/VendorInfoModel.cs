using System;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class VendorInfoModel
    {
        public int VendorId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string StoreLogoUrl { set; get; }
        public string StoreBannerUrl { get; set; }
        public string StoreTagLine { get; set; }
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
        public string RoutingNumber { get; set; }
        public string ChequeImageUrl { get; set; }
        public string[] VendorCertificateUrl { get; set; }
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
        public string ReturnPhone { get; set; }

        public string WarehousePhone { get; set; }
    }
}