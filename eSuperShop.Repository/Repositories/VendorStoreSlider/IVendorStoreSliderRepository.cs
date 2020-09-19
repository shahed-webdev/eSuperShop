using eSuperShop.Data;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface IVendorStoreSliderRepository
    {
        VendorStoreSlider VendorStoreSlider { get; set; }
        void Add(VendorSliderModel model);
        void Delete(int vendorStoreSliderId);
        bool IsNull(int vendorStoreSliderId);
        List<VendorSliderSlideModel> Display(int vendorId);
        List<VendorSliderModel> List(int vendorId);
    }
}