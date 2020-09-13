using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface ISpecificationRepository
    {
        AllSpecification Specification { get; set; }
        CatalogSpecification CatalogSpecification { get; set; }
        void Add(SpecificationAddModel model);
        void Delete(int id);
        SpecificationModel Get(int id);
        bool IsExistName(string name);
        bool IsExistName(string name, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
        DataResult<SpecificationModel> List(DataRequest request);
        List<DDL> ListDdl();
        void AssignCatalog(SpecificationAssignModel model);
    }
}