using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSuperShop.Repository
{
    public class BrandRepository : Repository, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }
        public AllBrand Brand { get; set; }
        public CatalogBrand CatalogBrand { get; set; }
        public void Add(BrandAddModel model)
        {
            Brand = _mapper.Map<AllBrand>(model);
            Db.AllBrand.Add(Brand);
        }

        public void Delete(int id)
        {
            Brand = Db.AllBrand.Find(id);
            Db.AllBrand.Remove(Brand);
        }

        public BrandModel Get(int id)
        {
            return Db.AllBrand
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
            return Db.CatalogBrand.Any(c => c.BrandId == id) || Db.Product.Any(c => c.BrandId == id);
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

        public async Task<ICollection<BrandModel>> SearchAsync(string key)
        {
            return await Db.AllBrand
                .Where(c => c.Name.Contains(key))
                .ProjectTo<BrandModel>(_mapper.ConfigurationProvider)
                .Take(5)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<ICollection<BrandModel>> SearchAsync(string key, int catalogId)
        {
            return await Db.CatalogBrand.Include(b => b.Brand)
                .Where(c => c.CatalogId == catalogId && c.Brand.Name.Contains(key))
                .Select(b => b.Brand)
                .ProjectTo<BrandModel>(_mapper.ConfigurationProvider)
                .Take(5)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public void AssignCatalog(BrandAssignModel model)
        {
            CatalogBrand = _mapper.Map<CatalogBrand>(model);
            Db.CatalogBrand.Add(CatalogBrand);
        }

        public void AssignCatalogMultiple(BrandAssignMultipleModel model)
        {
            var assignList = new List<CatalogBrand>();

            foreach (var brandId in model.BrandIds)
            {
                foreach (var catalogId in model.CatalogIds)
                {
                    if (!IsExistBrandInCatalog(brandId, catalogId))
                    {
                        assignList.Add(new CatalogBrand
                        {
                            CatalogId = catalogId,
                            BrandId = brandId,
                            AssignedByRegistrationId = model.AssignedByRegistrationId
                        });
                    }
                }
            }

            if (assignList.Any())
            {
                Db.CatalogBrand.AddRange(assignList);
            }
        }

        public void UnAssignCatalog(int brandId, int catalogId)
        {
            CatalogBrand = Db.CatalogBrand.FirstOrDefault(c => c.BrandId == brandId && c.CatalogId == catalogId);
            Db.CatalogBrand.Remove(CatalogBrand);
        }

        public bool IsExistBrandInCatalog(int brandId, int catalogId)
        {
            return Db.CatalogBrand.Any(c => c.BrandId == brandId && c.CatalogId == catalogId);
        }

        public List<BrandModel> CatalogWiseList(int catalogId)
        {
            var list = Db.CatalogBrand
                  .Include(c => c.Brand)
                  .Where(c => c.CatalogId == catalogId)
                  .Select(c => c.Brand)
                  .ProjectTo<BrandModel>(_mapper.ConfigurationProvider);
            return list.ToList();
        }

        public List<BrandModel> CatalogsProductWiseList(List<int> catalogIds)
        {
            var list = Db.Product
                .Include(c => c.Brand)
                .Where(p => p.Published && catalogIds.Contains(p.CatalogId))
                .Select(c => c.Brand)
                .Distinct()
                .ProjectTo<BrandModel>(_mapper.ConfigurationProvider);
            return list.ToList();
        }
    }
}