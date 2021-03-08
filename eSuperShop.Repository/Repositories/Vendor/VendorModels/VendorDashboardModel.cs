using System.Collections.Generic;

namespace eSuperShop.Repository
{
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