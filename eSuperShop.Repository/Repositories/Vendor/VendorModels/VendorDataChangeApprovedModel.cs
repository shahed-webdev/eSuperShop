namespace eSuperShop.Repository
{
    public class VendorDataChangeApprovedModel
    {
        public int VendorId { get; set; }
        public string StoreLogoUrl { set; get; }
        public string StoreBannerUrl { get; set; }
        public string StoreTagLine { get; set; }
        public string ChangedStoreBannerUrl { get; set; }
        public string ChangedStoreLogoUrl { get; set; }
        public string ChangedStoreTagLine { get; set; }
        public bool IsChangedApproved { get; set; }
    }
}