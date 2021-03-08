using eSuperShop.Data;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
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
}