using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class CatalogRepository : Repository, ICatalogRepository
    {
        public CatalogRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }


        public Catalog Catalog { get; set; }
        public CatalogShownPlace CatalogShownPlace { get; set; }
        public void Add(CatalogAddModel model)
        {
            Catalog = _mapper.Map<Catalog>(model);
            Db.Catalog.Add(Catalog);
        }

        public void Delete(int id)
        {
            Catalog = Db.Catalog.Find(id);
            Db.Catalog.Remove(Catalog);
        }

        public bool IsExistSlugUrl(string slugUrl)
        {
            return Db.Catalog.Any(c => c.SlugUrl == slugUrl);
        }

        public bool IsExistSlugUrl(string slugUrl, int updateId)
        {
            return Db.Catalog.Any(c => c.SlugUrl == slugUrl && c.CatalogId != updateId);
        }

        public bool IsExistName(string name)
        {
            return Db.Catalog.Any(c => c.CatalogName == name);
        }

        public bool IsExistName(string name, int updateId)
        {
            return Db.Catalog.Any(c => c.CatalogName == name && c.CatalogId != updateId);
        }

        public bool IsIsNull(int id)
        {
            return Db.Catalog.Any(c => c.CatalogId == id);
        }

        public bool IsRelatedDataExist(int id)
        {
            return Db.Catalog.Any(c => c.ParentCatalogId == id) || Db.CatalogShownPlace.Any(c => c.CatalogId == id);
        }

        public List<CatalogDisplayModel> Display(CatalogDisplayPlace place, int numberOfItem)
        {
            return Db.CatalogShownPlace.Include(c => c.Catalog)
                .Where(c => c.ShownPlace == place)
                .Take(numberOfItem)
                .Select(c => c.Catalog)
                .ProjectTo<CatalogDisplayModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public List<CatalogDisplayModel> DisplaySubCatalog(int parentCatalogId, int numberOfItem)
        {
            return Db.Catalog
                .Where(c => c.ParentCatalogId == parentCatalogId)
                .Take(numberOfItem)
                .ProjectTo<CatalogDisplayModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public List<CatalogModel> List()
        {
            return Db.Catalog
                .ProjectTo<CatalogModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public List<DDL> SliderPlaceDdl()
        {
            var list = from CatalogDisplayPlace a in Enum.GetValues(typeof(CatalogDisplayPlace))
                       select
                           new DDL
                           {
                               label = a.GetDescription(),
                               value = a.ToString()
                           };
            return list.ToList();
        }




        public List<DDL> ListDdl()
        {
            var ddl = Db.Catalog
                .AsEnumerable()?
                .ToList().OrderBy(c => c.ParentCatalogId).ThenBy(c => c.CatalogName)
                .Select(c => new DDL
                {
                    value = c.CatalogId.ToString(),
                    label = CatalogDllFunction(c.ParentCatalog, c.CatalogName)
                });

            return ddl?.ToList() ?? new List<DDL>();
        }

        public string Breadcrumb(int id)
        {
            return Db.Catalog
                .Where(c => c.CatalogId == id)
                .AsEnumerable()?
                .Select(c => CatalogDllFunction(c.ParentCatalog, c.CatalogName))
                .FirstOrDefault();
        }

        public void PlaceAssign(CatalogAssignModel model)
        {
            if (this.IsPlaceAssign(model.CatalogId, model.ShownPlace))
            {
                CatalogShownPlace.DisplayOrder = model.DisplayOrder;
                Db.CatalogShownPlace.Update(CatalogShownPlace);
            }
            else
            {
                CatalogShownPlace = _mapper.Map<CatalogShownPlace>(model);
                Db.CatalogShownPlace.Add(CatalogShownPlace);
            }

        }

        public bool IsPlaceAssign(int catalogId, CatalogDisplayPlace shownPlace)
        {
            CatalogShownPlace = Db.CatalogShownPlace.FirstOrDefault(c => c.CatalogId == catalogId && c.ShownPlace == shownPlace);
            return CatalogShownPlace != null;
        }

        public void PlaceDelete(int catalogId, CatalogDisplayPlace shownPlace)
        {
            Db.CatalogShownPlace.Remove(CatalogShownPlace);
        }

        string CatalogDllFunction(Catalog catalog, string cat)
        {
            if (catalog != null)
            {
                cat = CatalogDllFunction(catalog.ParentCatalog, catalog.CatalogName) + ">" + cat;
            }
            return cat;
        }
    }
}