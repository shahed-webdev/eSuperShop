using System.Collections.Generic;

namespace eSuperShop.Repository
{
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
}