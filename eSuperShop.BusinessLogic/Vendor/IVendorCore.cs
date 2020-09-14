using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;

namespace eSuperShop.BusinessLogic
{
    public interface IVendorCore
    {
        DbResponse<string> SendCode(string mobileNumber);
        DbResponse VerifyMobileNumber(string code);
        DbResponse<VendorModel> Add(VendorAddModel model);
        //Unapproved list & Approved list
        DataResult<VendorModel> List(DataRequest request);
        DbResponse AssignCatalog(VendorAssignCatalogModel model);
        DbResponse Approved(int vendorId);
        DbResponse Unapproved(int vendorId);
        DbResponse<VendorDetailsModel> GetDetails(int vendorId);



        //DbResponse Delete(int id);
        //DbResponse<List<DDL>> Ddl();
    }


}