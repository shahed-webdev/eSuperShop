namespace eSuperShop.Repository
{
    public class VendorDataChangeApprovedModel
    {
        public int VendorId { get; set; }
        public string StoreLogoFileName { set; get; }
        public string StoreBannerFileName { get; set; }
        public string StoreTagLine { get; set; }
        public string ChangedStoreBannerFileName { get; set; }
        public string ChangedStoreLogoFileName { get; set; }
        public string ChangedStoreTagLine { get; set; }
        public bool IsChangedApproved { get; set; }
    }
}