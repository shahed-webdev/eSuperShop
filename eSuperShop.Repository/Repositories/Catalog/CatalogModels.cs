using eSuperShop.Data;
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

    public class CatalogHierarchyModel
    {
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public CatalogHierarchyModel ParentCatalog { get; set; }
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

    public class CatalogVendorModel : ICatalogVendorModel
    {
        public CatalogVendorModel(Catalog catalog, int vendorId)
        {
            CatalogName = catalog.CatalogName;
            CatalogId = catalog.CatalogId;
            SlugUrl = catalog.SlugUrl;
            ImageUrl = catalog.ImageUrl;
            var vendorCatalog = catalog.VendorCatalog.FirstOrDefault(b => b.VendorId == vendorId && b.CatalogId == catalog.CatalogId);
            IsExist = vendorCatalog != null;
            CommissionPercentage = vendorCatalog?.CommissionPercentage ?? 0;
            SubCatalog = catalog.SubCatalog.Select(c => new CatalogVendorModel(c, vendorId));
        }
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string SlugUrl { get; set; }
        public string ImageUrl { get; set; }
        public bool IsExist { get; set; }
        public decimal CommissionPercentage { get; set; }
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
    public class CatalogAssignDetailsModel
    {
        public CatalogAssignDetailsModel()
        {
            Brands = new HashSet<BrandModel>();
            Specifications = new HashSet<SpecificationModel>();
            Attributes = new HashSet<AttributeModel>();
        }
        public CatalogDisplayModel CatalogInfo { get; set; }
        public ICollection<BrandModel> Brands { get; set; }
        public ICollection<SpecificationModel> Specifications { get; set; }
        public ICollection<AttributeModel> Attributes { get; set; }
    }

    public class CatalogProductListViewModel
    {
        public CatalogProductListViewModel()
        {
            CatalogIds = new List<int>();
            SubCatalogs = new HashSet<CatalogDisplayModel>();
            Brands = new HashSet<BrandModel>();
            Attributes = new HashSet<AttributeFilterModel>();
            Specifications = new HashSet<SpecificationFilterModel>();
            Products = new HashSet<ProductListViewModel>();
        }
        public List<int> CatalogIds { get; set; }
        public CatalogHierarchyModel Breadcrumb { get; set; }
        public ICollection<CatalogDisplayModel> SubCatalogs { get; set; }
        public ICollection<BrandModel> Brands { get; set; }
        public ICollection<AttributeFilterModel> Attributes { get; set; }
        public ICollection<SpecificationFilterModel> Specifications { get; set; }
        public ICollection<ProductListViewModel> Products { get; set; }
    }
}