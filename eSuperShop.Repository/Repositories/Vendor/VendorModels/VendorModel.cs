using System;

namespace eSuperShop.Repository
{
    public class VendorModel
    {
        public int VendorId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string StoreRegion { get; set; }
        public string StoreArea { get; set; }
        public string StoreAddress { get; set; }
        public string StorePostcode { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}