using CloudStorage;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public interface IVendorSliderCore
    {
        Task<DbResponse<VendorSliderModel>> AddAsync(VendorSliderModel model, string vendorUserName, ICloudStorage cloudStorage, IFormFile file);
        DbResponse Delete(int vendorStoreSliderId);
        DbResponse<List<VendorSliderSlideModel>> Display(string vendorUserName);
        DbResponse<List<VendorSliderModel>> List(string vendorUserName);
    }
}