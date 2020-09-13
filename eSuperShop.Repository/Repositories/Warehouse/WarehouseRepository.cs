using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class WarehouseRepository : Repository, IWarehouseRepository
    {
        public WarehouseRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public Warehouse Warehouse { get; set; }
        public VendorWarehouse CatalogWarehouse { get; set; }
        public void Add(WarehouseAddModel model)
        {
            Warehouse = _mapper.Map<Warehouse>(model);
            Db.Warehouse.Add(Warehouse);
        }

        public void Delete(int id)
        {
            Warehouse = Db.Warehouse.Find(id);
            Db.Warehouse.Remove(Warehouse);
        }

        public WarehouseModel Get(int id)
        {
            return Db.Warehouse
                .ProjectTo<WarehouseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault(c => c.WarehouseId == id);
        }

        public bool IsExistName(string name)
        {
            return Db.Warehouse.Any(c => c.Name == name);
        }

        public bool IsExistName(string name, int updateId)
        {
            return Db.Warehouse.Any(c => c.Name == name && c.WarehouseId != updateId);
        }

        public bool IsNull(int id)
        {
            return !Db.Warehouse.Any(c => c.WarehouseId == id);
        }

        public bool IsRelatedDataExist(int id)
        {
            return Db.VendorWarehouse.Any(c => c.WarehouseId == id);
        }

        public DataResult<WarehouseModel> List(DataRequest request)
        {
            var list = Db.Warehouse
                .ProjectTo<WarehouseModel>(_mapper.ConfigurationProvider);

            return list.ToDataResult(request);
        }

        public List<DDL> ListDdl()
        {
            var ddl = Db.Warehouse
                .ToList()
                .Select(c => new DDL
                {
                    value = c.WarehouseId.ToString(),
                    label = c.Name
                });

            return ddl?.ToList() ?? new List<DDL>();
        }

        public void AssignCatalog(WarehouseAssignModel model)
        {
            CatalogWarehouse = _mapper.Map<VendorWarehouse>(model);
            Db.VendorWarehouse.Add(CatalogWarehouse);
        }
    }
}