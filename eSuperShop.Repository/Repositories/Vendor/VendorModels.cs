using System;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public class VendorAddModel
    {
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string StoreAddress { get; set; }
    }
    public class VendorModel
    {
        public int VendorId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string StoreAddress { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
    public class VendorCatalogAssignModel
    {
        public VendorCatalogAssignModel()
        {
            Catalogs = new HashSet<VendorCatalogModel>();
        }
        public int VendorId { get; set; }

        public int AssignedByRegistrationId { get; set; }
        public ICollection<VendorCatalogModel> Catalogs { get; set; }


    }
    public class VendorCatalogModel
    {
        public int CatalogId { get; set; }
        public decimal CommissionPercentage { get; set; }
    }
    public class VendorApprovedModel
    {
        public int VendorId { get; set; }
        public string Email { get; set; }
        public int ApprovedByRegistrationId { get; set; }
    }
}