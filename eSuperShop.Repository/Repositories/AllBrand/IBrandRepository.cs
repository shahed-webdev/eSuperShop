﻿using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.Repository
{
    public interface IBrandRepository
    {
        AllBrand Brand { get; set; }
        CatalogBrand CatalogBrand { get; set; }

        void Add(BrandAddModel model);

        void Edit(BrandEditModel model);

        void Delete(int id);
        BrandModel Get(int id);
        bool IsExistName(string name);
        bool IsExistName(string name, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
        DataResult<BrandModel> List(DataRequest request);
        List<DDL> ListDdl();
        Task<ICollection<BrandModel>> SearchAsync(string key);
        Task<ICollection<BrandModel>> SearchAsync(string key, int catalogId);

        void AssignCatalog(BrandAssignModel model);
        void AssignCatalogMultiple(BrandAssignMultipleModel model);
        void UnAssignCatalog(int brandId, int catalogId);
        bool IsExistBrandInCatalog(int brandId, int catalogId);
        List<BrandModel> CatalogWiseList(int catalogId);
        List<DDL> CatalogWiseDdl(int catalogId);
        List<BrandModel> CatalogsProductWiseList(List<int> catalogIds);
    }
}