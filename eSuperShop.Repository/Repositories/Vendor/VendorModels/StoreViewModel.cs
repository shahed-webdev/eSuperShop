namespace eSuperShop.Repository
{
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
}