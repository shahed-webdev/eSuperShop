﻿using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using Paging.Infrastructure;
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
        bool IsExistStore(string store);
        bool IsExistStore(string store, int updateVendorId);
        bool IsExistSlugUrl(string slugUrl);
        bool IsExistSlugUrl(string slugUrl, int updateVendorId);
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
        bool IsCatalogExist(int vendorId, int catalogId);
        List<DDL> ThemeDdl();
        void ThemeChange(int vendorId, StoreTheme theme);
        void SlugUrlChange(int vendorId, string slugUrl);
        void BanarUrlChange(int vendorId, string banarUrl);
        void StoreInfoUpdate(VendorInfoUpdateModel model);
        VendorInfoModel ProfileDetails(int vendorId);
        PagedResult<StoreViewModel> TopStores(StoreFilterRequest request);
        StoreThemeViewModel StoreThemeDetails(string storeSlugUrl);


        PagedResult<StoreProductViewModel> StoreProductList(StoreProductFilterRequest request);
        DataResult<VendorDataChangeApprovedModel> DataChangeUnapprovedList(DataRequest request);
        //return list will be use for delete image from blob server 
        List<string> DataChangeApproved(int vendorId);
        List<string> DataChangeReject(int vendorId);

        void VendorInfoUpdateByAdmin(VendorInfoUpdateByAdminModel model);
    }
}