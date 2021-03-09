namespace eSuperShop.Repository
{
    public class VendorSliderModel
    {
        public int VendorStoreSliderId { get; set; }
        public int VendorId { get; set; }
        public string ImageFileName { get; set; }
        public string RedirectUrl { get; set; }
        public int? DisplayOrder { get; set; }
    }
    public class VendorSliderSlideModel
    {
        public int VendorStoreSliderId { get; set; }
        public string ImageFileName { get; set; }
        public string RedirectUrl { get; set; }
    }
}