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

    public class VendorCatalogViewModel
    {
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public decimal CommissionPercentage { get; set; }
    }
    public class VendorApprovedModel
    {
        public int VendorId { get; set; }
        public int ApprovedByRegistrationId { get; set; }
    }

    public class VendorInfoModel
    {
        public int VendorId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string StoreAddress { get; set; }
        public bool IsApproved { get; set; }
        public decimal GrossSale { get; set; }
        public decimal Discount { get; set; }
        public decimal Refund { get; set; }
        public decimal NetSale { get; set; }
        public decimal Commission { get; set; }
        public decimal Withdraw { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }

    public class VendorDashboardModel
    {
        public VendorDashboardModel()
        {
            Catalogs = new HashSet<VendorCatalogViewModel>();
        }

        public VendorInfoModel VendorInfo { get; set; }
        public ICollection<VendorCatalogViewModel> Catalogs { get; set; }
    }
}