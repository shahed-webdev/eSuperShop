using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IVendorSliderCore
    {
        DbResponse<VendorSliderModel> Add(VendorSliderModel model);
        DbResponse Delete(int vendorStoreSliderId);
        DbResponse<List<VendorSliderSlideModel>> Display(string vendorUserName);
        DbResponse<List<VendorSliderModel>> List(string vendorUserName);
    }
}