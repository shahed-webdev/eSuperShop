using AutoMapper;
using CloudStorage;
using eSuperShop.Data;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Http;
using Paging.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public class CatalogCore : ICatalogCore
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public CatalogCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<DbResponse<CatalogDisplayModel>> AddAsync(CatalogAddModel model, string userName, ICloudStorage cloudStorage, IFormFile image)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse<CatalogDisplayModel>(false, "Invalid User");

                model.CreatedByRegistrationId = registrationId;

                if (string.IsNullOrEmpty(model.CatalogName) && string.IsNullOrEmpty(model.SlugUrl))
                    return new DbResponse<CatalogDisplayModel>(false, "Invalid Data");

                if (_db.Catalog.IsExistName(model.CatalogName))
                    return new DbResponse<CatalogDisplayModel>(false, "Catalog Name already Exist", null, "CatalogName");

                if (_db.Catalog.IsExistSlugUrl(model.SlugUrl))
                    return new DbResponse<CatalogDisplayModel>(false, "SlugUrl already Exist", null, "SlugUrl");


                if (image != null)
                {
                    var fileName = FileBuilder.FileNameImage("catalog", image.FileName);
                    model.ImageFileName = await cloudStorage.UploadFileAsync(image, fileName);
                }

                _db.Catalog.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<CatalogDisplayModel>(_db.Catalog.catalog);

                return new DbResponse<CatalogDisplayModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<CatalogDisplayModel>(false, e.Message);
            }
        }

        public async Task<DbResponse> EditAsync(CatalogDisplayModel model, ICloudStorage cloudStorage, IFormFile image)
        {
            try
            {
                if (string.IsNullOrEmpty(model.CatalogName) && string.IsNullOrEmpty(model.SlugUrl))
                    return new DbResponse(false, "Invalid Data");

                if (_db.Catalog.IsExistName(model.CatalogName, model.CatalogId))
                    return new DbResponse(false, $"{model.CatalogName} already Exist");

                if (_db.Catalog.IsExistSlugUrl(model.SlugUrl, model.CatalogId))
                    return new DbResponse(false, $"{model.SlugUrl} SlugUrl already Exist");


                model.ImageFileName = await cloudStorage.UpdateFileAsync(image, model.ImageFileName, "catalog");

                _db.Catalog.Edit(model);
                _db.SaveChanges();



                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<CatalogDisplayModel> Get(int id)
        {
            try
            {
                if (_db.Catalog.IsNull(id))
                    return new DbResponse<CatalogDisplayModel>(false, "Data not found");
                var data = _db.Catalog.Get(id);
                return new DbResponse<CatalogDisplayModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<CatalogDisplayModel>(false, e.Message);
            }
        }

        public async Task<DbResponse> DeleteAsync(int id, ICloudStorage cloudStorage)
        {
            try
            {
                if (_db.Catalog.IsNull(id))
                    return new DbResponse(false, "Data not found");
                if (_db.Catalog.IsRelatedDataExist(id))
                    return new DbResponse(false, "Related data Exist");
                var catalog = _db.Catalog.Get(id);

                _db.Catalog.Delete(id);
                _db.SaveChanges();

                await cloudStorage.DeleteFileAsync(catalog.ImageFileName);

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<List<CatalogDisplayModel>> GetDisplayList(CatalogDisplayPlace place, int numberOfItem)
        {
            try
            {
                var data = _db.Catalog.DisplayList(place, numberOfItem);
                return new DbResponse<List<CatalogDisplayModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<CatalogDisplayModel>>(false, e.Message);
            }
        }

        public DbResponse<List<CatalogDisplayModel>> GetDisplayList(CatalogDisplayPlace place)
        {
            try
            {
                var data = _db.Catalog.DisplayList(place);
                return new DbResponse<List<CatalogDisplayModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<CatalogDisplayModel>>(false, e.Message);
            }
        }

        public DbResponse<List<ICatalogModel>> List()
        {
            try
            {
                var data = _db.Catalog.List();
                return new DbResponse<List<ICatalogModel>>(true, "Success", data.ToList());
            }
            catch (Exception e)
            {
                return new DbResponse<List<ICatalogModel>>(false, e.Message);
            }
        }

        public DbResponse<List<ICatalogModel>> BrandWiseList(int brandId)
        {
            try
            {
                var data = _db.Catalog.BrandWiseList(brandId);
                return new DbResponse<List<ICatalogModel>>(true, "Success", data.ToList());
            }
            catch (Exception e)
            {
                return new DbResponse<List<ICatalogModel>>(false, e.Message);
            }
        }

        public DbResponse<List<ICatalogModel>> AttributeWiseList(int attributeId)
        {
            try
            {
                var data = _db.Catalog.AttributeWiseList(attributeId);
                return new DbResponse<List<ICatalogModel>>(true, "Success", data.ToList());
            }
            catch (Exception e)
            {
                return new DbResponse<List<ICatalogModel>>(false, e.Message);
            }
        }

        public DbResponse<List<ICatalogModel>> SpecificationWiseList(int specificationId)
        {
            try
            {
                var data = _db.Catalog.SpecificationWiseList(specificationId);
                return new DbResponse<List<ICatalogModel>>(true, "Success", data.ToList());
            }
            catch (Exception e)
            {
                return new DbResponse<List<ICatalogModel>>(false, e.Message);
            }
        }

        public DbResponse<List<ICatalogVendorModel>> VendorWiseList(int vendorId)
        {
            try
            {
                var data = _db.Catalog.VendorWiseList(vendorId);
                return new DbResponse<List<ICatalogVendorModel>>(true, "Success", data.ToList());
            }
            catch (Exception e)
            {
                return new DbResponse<List<ICatalogVendorModel>>(false, e.Message);
            }
        }

        public DbResponse<CatalogAssignDetailsModel> AssignDetails(int catalogId)
        {
            try
            {
                var data = new CatalogAssignDetailsModel
                {
                    CatalogInfo = _db.Catalog.Get(catalogId),
                    Brands = _db.Brand.CatalogWiseList(catalogId),
                    Specifications = _db.Specification.CatalogWiseList(catalogId),
                    Attributes = _db.Attribute.CatalogWiseList(catalogId)
                }; return new DbResponse<CatalogAssignDetailsModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<CatalogAssignDetailsModel>(false, e.Message);
            }

        }

        public DbResponse<string> Breadcrumb(int id)
        {
            try
            {
                var data = _db.Catalog.Breadcrumb(id);
                return new DbResponse<string>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<string>(false, e.Message);
            }
        }

        public List<DDL> DisplayPlaceDdl()
        {
            return _db.Catalog.SliderPlaceDdl();
        }

        public DbResponse<List<DDL>> ListDdl()
        {
            try
            {
                var data = _db.Catalog.ListDdl();
                return new DbResponse<List<DDL>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<DDL>>(false, e.Message);
            }
        }

        public DbResponse<CatalogDisplayModel> AssignPlace(CatalogAssignModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse<CatalogDisplayModel>(false, "Invalid User");

                model.CreatedByRegistrationId = registrationId;

                _db.Catalog.PlaceAssign(model);
                _db.SaveChanges();



                var data = _db.Catalog.Get(model.CatalogId);

                return new DbResponse<CatalogDisplayModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<CatalogDisplayModel>(false, e.Message);
            }
        }

        public DbResponse<CatalogProductListViewModel> ProductListPageData(string slugUrl, int pageSize)
        {
            try
            {
                if (!_db.Catalog.IsExistSlugUrl(slugUrl))
                    return new DbResponse<CatalogProductListViewModel>(false, "Invalid Catalog");

                var data = new CatalogProductListViewModel();
                data.CatalogIds = _db.Catalog.CatalogIdsBySlugUrl(slugUrl);
                data.Breadcrumb = _db.Catalog.BreadcrumbBySlugUrl(slugUrl);
                data.SubCatalogs = _db.Catalog.DisplaySubCatalog(data.Breadcrumb.CatalogId, 15);
                data.Brands = _db.Brand.CatalogsProductWiseList(data.CatalogIds);
                data.Attributes = _db.Attribute.CatalogsProductWiseList(data.CatalogIds);
                data.Specifications = _db.Specification.CatalogsProductWiseList(data.CatalogIds);
                data.Products = _db.Product.GetCatalogWiseList(data.CatalogIds, new ProductFilterRequest
                {
                    Page = 1,
                    PageSize = pageSize
                }).Results;


                return new DbResponse<CatalogProductListViewModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<CatalogProductListViewModel>(false, e.Message);
            }
        }

        public DbResponse<PagedResult<ProductListViewModel>> GetCatalogWiseList(string slugUrl,
            ProductFilterRequest request)
        {
            try
            {
                if (!_db.Catalog.IsExistSlugUrl(slugUrl))
                    return new DbResponse<PagedResult<ProductListViewModel>>(false, "Invalid Catalog");

                var catalogIds = _db.Catalog.CatalogIdsBySlugUrl(slugUrl);

                var data = _db.Product.GetCatalogWiseList(catalogIds, request);


                return new DbResponse<PagedResult<ProductListViewModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<PagedResult<ProductListViewModel>>(false, e.Message);
            }
        }

        public DbResponse DeletePlace(int catalogId, CatalogDisplayPlace shownPlace)
        {
            try
            {
                if (!_db.Catalog.IsPlaceAssign(catalogId, shownPlace))
                    return new DbResponse(false, "Data not found");

                _db.Catalog.PlaceDelete(catalogId, shownPlace);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<CatalogShippingCostViewModel> GetShippingCost(int catalogId)
        {
            try
            {
                if (_db.Catalog.IsNull(catalogId))
                    return new DbResponse<CatalogShippingCostViewModel>(false, "Data not found");
                var data = _db.Catalog.GetShippingCost(catalogId);
                return new DbResponse<CatalogShippingCostViewModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<CatalogShippingCostViewModel>(false, e.Message);
            }
        }

        public DbResponse ShippingCostChanged(CatalogShippingCostViewModel model)
        {
            try
            {
                if (_db.Catalog.IsNull(model.CatalogId))
                    return new DbResponse(false, "Data not found");
                _db.Catalog.ShippingCostChanged(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }
    }
}