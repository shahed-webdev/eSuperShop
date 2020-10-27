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

        public IEnumerable<ICatalogModel> List()
        {
            var catalogs = Db.Catalog
                .AsEnumerable()?
                .Where(c => c.ParentCatalog == null)
                .ToList()
                .Select(c => new CatalogModel(c));

            return catalogs;

        }

        public IEnumerable<ICatalogModel> BrandWiseList(int brandId)
        {
            var catalogs = Db.Catalog
                .Include(c => c.CatalogBrand)
                .AsEnumerable()?
                .Where(c => c.ParentCatalog == null)
                .ToList()
                .Select(c => new CatalogBrandModel(c, brandId));

            return catalogs;
        }

        public IEnumerable<ICatalogModel> AttributeWiseList(int attributeId)
        {
            var catalogs = Db.Catalog
                .Include(c => c.CatalogAttribute)
                .AsEnumerable()?
                .Where(c => c.ParentCatalog == null)
                .ToList()
                .Select(c => new CatalogAttributeModel(c, attributeId));

            return catalogs;
        }

        public IEnumerable<ICatalogModel> SpecificationWiseList(int specificationId)
        {
            var catalogs = Db.Catalog
                .Include(c => c.CatalogSpecification)
                .AsEnumerable()?
                .Where(c => c.ParentCatalog == null)
                .ToList()
                .Select(c => new CatalogSpecificationModel(c, specificationId));

            return catalogs;
        }

        public IEnumerable<ICatalogVendorModel> VendorWiseList(int vendorId)
        {
            var catalogs = Db.Catalog
                .Include(c => c.VendorCatalog)
                .AsEnumerable()?
                .Where(c => c.ParentCatalog == null)
                .ToList()
                .Select(c => new CatalogVendorModel(c, vendorId));

            return catalogs;
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

        public CatalogHierarchyModel BreadcrumbBySlugUrl(string slugUrl)
        {
            return Db.Catalog
                .Include(c => c.ParentCatalog)
                .Where(c => c.SlugUrl == slugUrl)
                .ProjectTo<CatalogHierarchyModel>(_mapper.ConfigurationProvider)
                .AsEnumerable()?
                .FirstOrDefault();
        }

        public List<int> CatalogIdsBySlugUrl(string slugUrl)
        {
            var Ids = new List<int>();
            var cat = Db.Catalog
                .Include(c => c.SubCatalog)
                .Where(c => c.SlugUrl == slugUrl)
                .AsEnumerable()?
                .FirstOrDefault();

            if (cat != null)
                CatalogIdsFunction(cat.CatalogId, cat.SubCatalog, Ids);

            return Ids;
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
            var seo = Db.Catalog.Include(c => c.Seo).FirstOrDefault(c => c.CatalogId == id).Seo;
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

            if (catalog.Seo == null)
            {
                var seo = _mapper.Map<Seo>(model);
                catalog.Seo = seo;
            }
            else
            {
                catalog.Seo.MetaTitle = model.MetaTitle;
                catalog.Seo.MetaDescription = model.MetaDescription;
                catalog.Seo.MetaKeywords = model.MetaKeywords;
            }

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


        void CatalogIdsFunction(int catalogId, ICollection<Catalog> subCatalogs, List<int> ids)
        {
            ids.Add(catalogId);

            foreach (var subCatalog in subCatalogs)
            {
                if (subCatalog.SubCatalog != null)
                {
                    CatalogIdsFunction(subCatalog.CatalogId, subCatalog.SubCatalog, ids);
                }
            }
        }
    }
}