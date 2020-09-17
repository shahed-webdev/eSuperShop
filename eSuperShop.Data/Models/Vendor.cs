using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class Vendor
    {
        public Vendor()
        {
            Product = new HashSet<Product>();
            VendorCatalog = new HashSet<VendorCatalog>();
            VendorFollower = new HashSet<VendorFollower>();
            VendorReview = new HashSet<VendorReview>();
            VendorWarehouse = new HashSet<VendorWarehouse>();
        }

        public int VendorId { get; set; }
        public int? RegistrationId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string StoreAddress { get; set; }
        public StoreTheme StoreTheme { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedByRegistrationId { get; set; }
        public DateTime? ApprovedOnUtc { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public decimal GrossSale { get; set; }
        public decimal Discount { get; set; }
        public decimal Refund { get; set; }
        public decimal NetSale { get; set; }
        public decimal Commission { get; set; }
        public decimal Withdraw { get; set; }
        public decimal Balance { get; set; }

        public virtual Registration Registration { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<VendorCatalog> VendorCatalog { get; set; }
        public virtual ICollection<VendorFollower> VendorFollower { get; set; }
        public virtual ICollection<VendorReview> VendorReview { get; set; }
        public virtual ICollection<VendorWarehouse> VendorWarehouse { get; set; }
    }
}
