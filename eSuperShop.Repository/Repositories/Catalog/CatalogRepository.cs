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


        public Catalog catalog { get; set; }
        public CatalogShownPlace catalogShownPlace { get; set; }
        public void Add(CatalogAddModel model)
        {
            catalog = _mapper.Map<Catalog>(model);
            Db.Catalog.Add(catalog);
        }

        public void Delete(int id)
        {
            catalog = Db.Catalog.Find(id);
            Db.Catalog.Remove(catalog);
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

        public bool IsNull(int id)
        {
            return !Db.Catalog.Any(c => c.CatalogId == id);
        }

        public bool IsRelatedDataExist(int id)
        {
            return Db.Catalog.Any(c => c.ParentCatalogId == id) || Db.CatalogShownPlace.Any(c => c.CatalogId == id);
        }

        public List<CatalogDisplayModel> DisplayList(CatalogDisplayPlace place)
        {
            return Db.CatalogShownPlace.Include(c => c.Catalog)
                .Where(c => c.ShownPlace == place)
                .Select(c => c.Catalog)
                .ProjectTo<CatalogDisplayModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public List<CatalogDisplayModel> DisplayList(CatalogDisplayPlace place, int numberOfItem)
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

        public CatalogDisplayModel Get(int id)
        {
            return Db.Catalog.ProjectTo<CatalogDisplayModel>(_mapper.ConfigurationProvider).FirstOrDefault(c => c.CatalogId == id);
        }

        public void PlaceAssign(CatalogAssignModel model)
        {
            if (this.IsPlaceAssign(model.CatalogId, model.ShownPlace))
            {
                catalogShownPlace.DisplayOrder = model.DisplayOrder;
                Db.CatalogShownPlace.Update(catalogShownPlace);
            }
            else
            {
                catalogShownPlace = _mapper.Map<CatalogShownPlace>(model);
                Db.CatalogShownPlace.Add(catalogShownPlace);
            }

        }

        public bool IsPlaceAssign(int catalogId, CatalogDisplayPlace shownPlace)
        {
            catalogShownPlace = Db.CatalogShownPlace.FirstOrDefault(c => c.CatalogId == catalogId && c.ShownPlace == shownPlace);
            return catalogShownPlace != null;
        }

        public void PlaceDelete(int catalogId, CatalogDisplayPlace shownPlace)
        {
            Db.CatalogShownPlace.Remove(catalogShownPlace);
        }

        public SeoModel GetSeo(int id)
        {
            return Db.Catalog
                   .Where(c => c.CatalogId == id)
                   .Select(c => c.Seo)
                   .ProjectTo<SeoModel>(_mapper.ConfigurationProvider)
                   .FirstOrDefault();
        }

        public void SeoDelete(int id)
        {
            var seo = Db.Catalog.Find(id).Seo;
            Db.Seo.Remove(seo);
        }

        public bool IsSeoExist(int id)
        {
            return Db.Catalog.Any(c => c.CatalogId == id && c.SeoId != null);
        }

        public void PostSeo(SeoAddModel model)
        {
            var catalog = Db.Catalog.Include(c => c.Seo).FirstOrDefault(c => c.CatalogId == model.AssignTableId);
            if (catalog == null) return;
            var seo = _mapper.Map<Seo>(model);
            catalog.Seo = seo;
            Db.Catalog.Update(catalog);
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