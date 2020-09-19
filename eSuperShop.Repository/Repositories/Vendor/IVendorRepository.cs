using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.Repository
{
    public interface IVendorRepository
    {
        Vendor Vendor { get; set; }
        void Add(VendorAddModel model);
        VendorModel Get(int vendorId);
        VendorDashboardModel Dashboard(int vendorId);
        bool IsExistPhone(string phone);
        bool IsExistEmail(string email);
        bool IsNull(int vendorId);
        DataResult<VendorModel> List(DataRequest request);
        Task<ICollection<VendorModel>> SearchAsync(string key);
        void AssignCatalogMultiple(VendorCatalogAssignModel model);
        void UnAssignCatalog(int vendorId, int catalogId);
        bool IsExistVendorInCatalog(int vendorId, int catalogId);
        List<VendorModel> CatalogWiseList(int catalogId);
        void Approved(VendorApprovedModel model);
        void Delete(int vendorId);
        bool IsApproved(int vendorId);
        string GetPhone(int vendorId);
        List<VendorCatalogViewModel> Catalogs(int vendorId);
        List<DDL> ThemeDdl();
    }


}