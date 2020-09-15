﻿using eSuperShop.Data;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class CatalogAddModel
    {
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public int? ParentCatalogId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int? DisplayOrder { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }

    public class CatalogDisplayModel
    {
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CatalogModel : ICatalogModel
    {
        public CatalogModel(Catalog catalog)
        {
            CatalogName = catalog.CatalogName;
            CatalogId = catalog.CatalogId;
            SlugUrl = catalog.SlugUrl;
            ImageUrl = catalog.ImageUrl;
            SubCatalog = catalog.SubCatalog.Select(c => new CatalogModel(c));
        }
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public string ImageUrl { get; set; }
        public bool IsExist { get; set; }
        public IEnumerable<ICatalogModel> SubCatalog { get; set; }
    }


    public class CatalogBrandModel : ICatalogModel
    {
        public CatalogBrandModel(Catalog catalog, int brandId)
        {
            CatalogName = catalog.CatalogName;
            CatalogId = catalog.CatalogId;
            SlugUrl = catalog.SlugUrl;
            ImageUrl = catalog.ImageUrl;
            IsExist = catalog.CatalogBrand.Any(b => b.BrandId == brandId && b.CatalogId == catalog.CatalogId);
            SubCatalog = catalog.SubCatalog.Select(c => new CatalogBrandModel(c, brandId));
        }
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public string ImageUrl { get; set; }
        public bool IsExist { get; set; }
        public IEnumerable<ICatalogModel> SubCatalog { get; set; }
    }

    public class CatalogAttributeModel : ICatalogModel
    {
        public CatalogAttributeModel(Catalog catalog, int attributeId)
        {
            CatalogName = catalog.CatalogName;
            CatalogId = catalog.CatalogId;
            SlugUrl = catalog.SlugUrl;
            ImageUrl = catalog.ImageUrl;
            IsExist = catalog.CatalogAttribute.Any(b => b.AttributeId == attributeId && b.CatalogId == catalog.CatalogId);
            SubCatalog = catalog.SubCatalog.Select(c => new CatalogAttributeModel(c, attributeId));
        }
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public string ImageUrl { get; set; }
        public bool IsExist { get; set; }
        public IEnumerable<ICatalogModel> SubCatalog { get; set; }
    }
    public class CatalogSpecificationModel : ICatalogModel
    {
        public CatalogSpecificationModel(Catalog catalog, int specificationId)
        {
            CatalogName = catalog.CatalogName;
            CatalogId = catalog.CatalogId;
            SlugUrl = catalog.SlugUrl;
            ImageUrl = catalog.ImageUrl;
            IsExist = catalog.CatalogSpecification.Any(b => b.SpecificationId == specificationId && b.CatalogId == catalog.CatalogId);
            SubCatalog = catalog.SubCatalog.Select(c => new CatalogSpecificationModel(c, specificationId));
        }
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public string ImageUrl { get; set; }
        public bool IsExist { get; set; }
        public IEnumerable<ICatalogModel> SubCatalog { get; set; }
    }
    public class CatalogAssignModel
    {
        public int CatalogShownPlaceId { get; set; }
        public int CatalogId { get; set; }
        public CatalogDisplayPlace ShownPlace { get; set; }
        public int DisplayOrder { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }

}