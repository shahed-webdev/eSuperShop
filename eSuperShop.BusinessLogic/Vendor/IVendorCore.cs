using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;

namespace eSuperShop.BusinessLogic
{
    public interface IVendorCore
    {
        DbResponse SendCode(string mobileNumber, int codeValidSecond);
        DbResponse VerifyMobileNumber(string code, string mobileNumber);
        DbResponse<VendorModel> Add(VendorAddModel model);
        //Unapproved list & Approved list
        DataResult<VendorModel> List(DataRequest request);
        DbResponse Approved(int vendorId);
        DbResponse Unapproved(int vendorId);
        DbResponse<VendorDetailsModel> GetDetails(int vendorId);
        DbResponse AssignCatalogMultiple(VendorCatalogAssignModel model, string userName);
        DbResponse UnAssignCatalog(int vendorId, int catalogId);


        //DbResponse Delete(int id);
        //DbResponse<List<DDL>> Ddl();
    }


}