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
    public class SpecificationRepository : Repository, ISpecificationRepository
    {
        public SpecificationRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public AllSpecification Specification { get; set; }
        public CatalogSpecification CatalogSpecification { get; set; }
        public void Add(SpecificationAddModel model)
        {
            Specification = _mapper.Map<AllSpecification>(model);
            Db.AllSpecification.Add(Specification);
        }

        public void Delete(int id)
        {
            Specification = Db.AllSpecification.Find(id);
            Db.AllSpecification.Remove(Specification);
        }

        public SpecificationModel Get(int id)
        {
            return Db.AllSpecification
                .ProjectTo<SpecificationModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault(c => c.SpecificationId == id);
        }

        public bool IsExistName(string name)
        {
            return Db.AllSpecification.Any(c => c.KeyName == name);
        }

        public bool IsExistName(string name, int updateId)
        {
            return Db.AllSpecification.Any(c => c.KeyName == name && c.SpecificationId != updateId);
        }

        public bool IsNull(int id)
        {
            return !Db.AllSpecification.Any(c => c.SpecificationId == id);
        }

        public bool IsRelatedDataExist(int id)
        {
            return Db.ProductSpecification.Any(c => c.SpecificationId == id);
        }

        public DataResult<SpecificationModel> List(DataRequest request)
        {
            var list = Db.AllSpecification
                .ProjectTo<SpecificationModel>(_mapper.ConfigurationProvider);

            return list.ToDataResult(request);
        }

        public List<DDL> ListDdl()
        {
            var ddl = Db.AllSpecification
                .ToList()
                .Select(c => new DDL
                {
                    value = c.SpecificationId.ToString(),
                    label = c.KeyName
                });

            return ddl?.ToList() ?? new List<DDL>();
        }

        public async Task<ICollection<SpecificationModel>> SearchAsync(string key)
        {
            return await Db.AllSpecification
                .Where(c => c.KeyName.Contains(key))
                .ProjectTo<SpecificationModel>(_mapper.ConfigurationProvider)
                .Take(5)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public void AssignCatalog(SpecificationAssignModel model)
        {
            CatalogSpecification = _mapper.Map<CatalogSpecification>(model);
            Db.CatalogSpecification.Add(CatalogSpecification);
        }

        public void AssignCatalogMultiple(SpecificationAssignMultipleModel model)
        {
            var assignList = new List<CatalogSpecification>();

            foreach (var SpecificationId in model.SpecificationIds)
            {
                foreach (var catalogId in model.CatalogIds)
                {
                    if (!IsExistSpecificationInCatalog(SpecificationId, catalogId))
                    {
                        assignList.Add(new CatalogSpecification
                        {
                            CatalogId = catalogId,
                            SpecificationId = SpecificationId,
                            AssignedByRegistrationId = model.AssignedByRegistrationId
                        });
                    }
                }
            }

            if (assignList.Any())
            {
                Db.CatalogSpecification.AddRange(assignList);
            }
        }

        public void UnAssignCatalog(int SpecificationId, int catalogId)
        {
            CatalogSpecification = Db.CatalogSpecification.FirstOrDefault(c => c.SpecificationId == SpecificationId && c.CatalogId == catalogId);
            Db.CatalogSpecification.Remove(CatalogSpecification);
        }

        public bool IsExistSpecificationInCatalog(int SpecificationId, int catalogId)
        {
            return Db.CatalogSpecification.Any(c => c.SpecificationId == SpecificationId && c.CatalogId == catalogId);
        }
    }
}