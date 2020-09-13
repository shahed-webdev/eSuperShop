using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class BrandRepository : Repository, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }
        public AllBrand brand { get; set; }
        public CatalogBrand catalogBrand { get; set; }
        public void Add(BrandAddModel model)
        {
            brand = _mapper.Map<AllBrand>(model);
            Db.AllBrand.Add(brand);
        }

        public void Delete(int id)
        {
            brand = Db.AllBrand.Find(id);
            Db.AllBrand.Remove(brand);
        }

        public BrandModel Get(int id)
        {
            return Db.Catalog
                .ProjectTo<BrandModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault(c => c.BrandId == id);
        }

        public bool IsExistName(string name)
        {
            return Db.AllBrand.Any(c => c.Name == name);
        }

        public bool IsExistName(string name, int updateId)
        {
            return Db.AllBrand.Any(c => c.Name == name && c.BrandId != updateId);
        }

        public bool IsNull(int id)
        {
            return !Db.AllBrand.Any(c => c.BrandId == id);
        }

        public bool IsRelatedDataExist(int id)
        {
            return Db.Product.Any(c => c.BrandId == id);
        }

        public DataResult<BrandModel> List(DataRequest request)
        {
            var list = Db.AllBrand
                .ProjectTo<BrandModel>(_mapper.ConfigurationProvider);

            return list.ToDataResult(request);
        }

        public List<DDL> ListDdl()
        {
            var ddl = Db.AllBrand
                .ToList()
                .Select(c => new DDL
                {
                    value = c.BrandId.ToString(),
                    label = c.Name
                });

            return ddl?.ToList() ?? new List<DDL>();
        }

        public void AssignCatalog(BrandAssignModel model)
        {
            catalogBrand = _mapper.Map<CatalogBrand>(model);
            Db.CatalogBrand.Add(catalogBrand);
        }
    }
}