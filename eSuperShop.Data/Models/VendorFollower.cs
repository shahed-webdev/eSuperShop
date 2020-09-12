using System;

namespace eSuperShop.Data
{
    public class VendorFollower
    {
        public int VendorFollowerId { get; set; }
        public int VendorId { get; set; }
        public int CustomerId { get; set; }
        public DateTime FollowedOnUtc { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
