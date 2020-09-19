using eSuperShop.Repository;

namespace eSuperShop.BusinessLogic
{
    public interface IVendorDashboardCore
    {
        DbResponse<VendorDashboardModel> GetDetails(string userName);
    }


}