using System;

namespace eSuperShop.Data
{
    public class VendorStoreSlider
    {
        public int VendorStoreSliderId { get; set; }
        public int VendorId { get; set; }
        public string ImageUrl { get; set; }
        public string RedirectUrl { get; set; }
        public int? DisplayOrder { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual Vendor Vendor { get; set; }
        public bool IsApprovedByAdmin { get; set; }
    }
}