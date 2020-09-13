using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class AttributeRepository : Repository, IAttributeRepository
    {
        public AttributeRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }
        public AllAttribute Attribute { get; set; }
        public CatalogAttribute CatalogAttribute { get; set; }
        public void Add(AttributeAddModel model)
        {
            Attribute = _mapper.Map<AllAttribute>(model);
            Db.AllAttribute.Add(Attribute);
        }

        public void Delete(int id)
        {
            Attribute = Db.AllAttribute.Find(id);
            Db.AllAttribute.Remove(Attribute);
        }

        public AttributeModel Get(int id)
        {
            return Db.AllAttribute
                .ProjectTo<AttributeModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault(c => c.AttributeId == id);
        }

        public bool IsExistName(string name)
        {
            return Db.AllAttribute.Any(c => c.KeyName == name);
        }

        public bool IsExistName(string name, int updateId)
        {
            return Db.AllAttribute.Any(c => c.KeyName == name && c.AttributeId != updateId);
        }

        public bool IsNull(int id)
        {
            return !Db.AllAttribute.Any(c => c.AttributeId == id);
        }

        public bool IsRelatedDataExist(int id)
        {
            return Db.ProductAttribute.Any(c => c.AttributeId == id);
        }

        public DataResult<AttributeModel> List(DataRequest request)
        {
            var list = Db.AllAttribute
                .ProjectTo<AttributeModel>(_mapper.ConfigurationProvider);

            return list.ToDataResult(request);
        }

        public List<DDL> ListDdl()
        {
            var ddl = Db.AllAttribute
                .ToList()
                .Select(c => new DDL
                {
                    value = c.AttributeId.ToString(),
                    label = c.KeyName
                });

            return ddl?.ToList() ?? new List<DDL>();
        }

        public void AssignCatalog(AttributeAssignModel model)
        {
            CatalogAttribute = _mapper.Map<CatalogAttribute>(model);
            Db.CatalogAttribute.Add(CatalogAttribute);
        }
    }
}