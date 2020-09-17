﻿using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.Repository
{
    public interface IVendorRepository
    {
        Vendor Vendor { get; set; }
        void Add(VendorAddModel model);
        VendorModel Get(int id);
        bool IsExistPhone(string phone);
        bool IsExistEmail(string email);
        bool IsNull(int id);
        DataResult<VendorModel> List(DataRequest request);
        Task<ICollection<VendorModel>> SearchAsync(string key);
        void AssignCatalogMultiple(VendorCatalogAssignModel model);
        void UnAssignCatalog(int VendorId, int catalogId);
        bool IsExistVendorInCatalog(int VendorId, int catalogId);
        List<VendorModel> CatalogWiseList(int catalogId);
        void Approved(VendorApprovedModel model);
        void Delete(int vendorId);
        bool IsApproved(int vendorId);
        string GetPhone(int vendorId);
    }
}