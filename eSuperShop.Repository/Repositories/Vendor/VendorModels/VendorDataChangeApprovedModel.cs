namespace eSuperShop.Repository
{
    public class VendorDataChangeApprovedModel
    {
        public int VendorId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string Email { get; set; }
        public string StoreName { get; set; }
        public string StoreLogoFileName { set; get; }
        public string StoreBannerFileName { get; set; }
        public string StoreTagLine { get; set; }
        public string ChangedStoreBannerFileName { get; set; }
        public string ChangedStoreLogoFileName { get; set; }
        public string ChangedStoreTagLine { get; set; }
        public bool IsChangedApproved { get; set; }
    }
}