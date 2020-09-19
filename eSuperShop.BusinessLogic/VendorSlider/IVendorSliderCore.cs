using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IVendorSliderCore
    {
        DbResponse<VendorSliderModel> Add(VendorSliderModel model);
        DbResponse Delete(int vendorStoreSliderId);
        DbResponse<List<VendorSliderSlideModel>> Display(int vendorId);
        DbResponse<List<VendorSliderModel>> List(int vendorId);
    }
}