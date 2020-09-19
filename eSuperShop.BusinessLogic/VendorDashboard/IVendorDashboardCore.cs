using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IVendorDashboardCore
    {
        DbResponse<VendorDashboardModel> GetDetails(string userName);
        List<DDL> ThemeDdl();
    }


}