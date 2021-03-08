namespace eSuperShop.Repository
{
    public class VendorInfoUpdateModel
    {
        public int VendorId { get; set; }
        public string StoreLogoUrl { set; get; }
        public string StoreBannerUrl { get; set; }
        public string StoreTagLine { get; set; }
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
        public int WarehouseAreaId { get; set; }
        public string ReturnAddress { get; set; }

        public string ReturnPhone { get; set; }
        public string ReturnPostcode { get; set; }
        public int ReturnAreaId { get; set; }
        public string[] VendorCertificateUrl { get; set; }
    }
}