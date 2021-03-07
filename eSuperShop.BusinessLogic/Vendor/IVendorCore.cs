using eSuperShop.Data;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public interface IVendorCore
    {
        DbResponse SendCode(string mobileNumber, int codeValidSecond);
        DbResponse VerifyMobileNumber(string code, string mobileNumber);
        DbResponse<VendorModel> Add(VendorAddModel model);
        //Unapproved list & Approved list
        DataResult<VendorModel> List(DataRequest request);
        Task<DbResponse> ApprovedAsync(VendorApprovedModel model, string userName);
        DbResponse Delete(int vendorId);
        DbResponse<VendorModel> GetDetails(int vendorId);
        DbResponse AssignCatalogMultiple(VendorCatalogAssignModel model, string userName);
        DbResponse UnAssignCatalog(int vendorId, int catalogId);


        //DbResponse Delete(int id);
        List<DDL> ThemeDdl();
        DbResponse ThemeChange(int vendorId, StoreTheme theme);
        DbResponse SlugUrlChange(int vendorId, string slugUrl);
        DbResponse BannerUrlChange(int vendorId, string bannerUrl);
        DbResponse StoreUpdate(VendorInfoUpdateModel model, string vendorUserName);
        DbResponse<VendorInfoModel> ProfileDetails(string vendorUserName);
    }
}