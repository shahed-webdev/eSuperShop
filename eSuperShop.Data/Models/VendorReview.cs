using System;

namespace eSuperShop.Data
{
    public class VendorReview
    {
        public int VendorReviewId { get; set; }
        public int VendorId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public DateTime ReviewedOnUtc { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
