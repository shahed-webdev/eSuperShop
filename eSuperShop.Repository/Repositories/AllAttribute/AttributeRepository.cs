﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            return ddl.ToList();
        }

        public async Task<ICollection<AttributeModel>> SearchAsync(string key)
        {
            return await Db.AllAttribute
                .Where(c => c.KeyName.Contains(key))
                .ProjectTo<AttributeModel>(_mapper.ConfigurationProvider)
                .Take(5)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<ICollection<AttributeModel>> SearchAsync(string key, int catalogId)
        {
            return await Db.CatalogAttribute
                .Include(c => c.Attribute)
                .Where(c => c.CatalogId == catalogId && c.Attribute.KeyName.Contains(key))
                .Select(c => c.Attribute)
                .ProjectTo<AttributeModel>(_mapper.ConfigurationProvider)
                .Take(5)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public void AssignCatalog(AttributeAssignModel model)
        {
            CatalogAttribute = _mapper.Map<CatalogAttribute>(model);
            Db.CatalogAttribute.Add(CatalogAttribute);
        }

        public void AssignCatalogMultiple(AttributeAssignMultipleModel model)
        {
            var assignList = new List<CatalogAttribute>();

            foreach (var attributeId in model.AttributeIds)
            {
                foreach (var catalogId in model.CatalogIds)
                {
                    if (!IsExistAttributeInCatalog(attributeId, catalogId))
                    {
                        assignList.Add(new CatalogAttribute
                        {
                            CatalogId = catalogId,
                            AttributeId = attributeId,
                            AssignedByRegistrationId = model.AssignedByRegistrationId
                        });
                    }
                }
            }

            if (assignList.Any())
            {
                Db.CatalogAttribute.AddRange(assignList);
            }
        }

        public void UnAssignCatalog(int attributeId, int catalogId)
        {
            CatalogAttribute = Db.CatalogAttribute.FirstOrDefault(c => c.AttributeId == attributeId && c.CatalogId == catalogId);
            Db.CatalogAttribute.Remove(CatalogAttribute);
        }

        public bool IsExistAttributeInCatalog(int attributeId, int catalogId)
        {
            return Db.CatalogAttribute.Any(c => c.AttributeId == attributeId && c.CatalogId == catalogId);
        }

        public List<AttributeModel> CatalogWiseList(int catalogId)
        {
            var list = Db.CatalogAttribute
                .Include(c => c.Attribute)
                .Where(c => c.CatalogId == catalogId)
                .Select(c => c.Attribute)
                .ProjectTo<AttributeModel>(_mapper.ConfigurationProvider);
            return list.ToList();
        }

        public List<DDL> CatalogWiseDdl(int catalogId)
        {
            var list = Db.CatalogAttribute
                .Include(c => c.Attribute)
                .Where(c => c.CatalogId == catalogId)
                .Select(c => new DDL
                {
                    value = c.AttributeId.ToString(),
                    label = c.Attribute.KeyName
                });
            return list.ToList();
        }

        public List<AttributeFilterModel> CatalogsProductWiseList(List<int> catalogIds)
        {
            var list = Db.ProductAttributeValue
                .Include(s => s.ProductAttribute)
                .ThenInclude(a => a.Attribute)
                .Where(p => p.ProductAttribute.Product.Published && p.ProductAttribute.Attribute.AllowFiltering && catalogIds.Contains(p.ProductAttribute.Product.CatalogId))
                .Select(s => new
                {
                    Attribute = s.ProductAttribute.Attribute,
                    Value = s.Value
                }).ToList();

            var attribute = list.GroupBy(
                p => p.Attribute,
                p => p.Value,
                (s, g) => new AttributeFilterModel
                {
                    AttributeId = s.AttributeId,
                    KeyName = s.KeyName,
                    Values = g.Distinct().OrderBy(v => v).ToArray()
                }).OrderBy(a => a.KeyName).ToList();
            return attribute;
        }
    }
}