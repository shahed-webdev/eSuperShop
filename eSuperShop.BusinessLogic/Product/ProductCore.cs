using AutoMapper;
using CloudStorage;
using eSuperShop.Repository;
using Paging.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public class ProductCore : IProductCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;
        private readonly ICloudStorage _cloudStorage;

        public ProductCore(IMapper mapper, IUnitOfWork db, ICloudStorage cloudStorage)
        {
            _mapper = mapper;
            _db = db;
            _cloudStorage = cloudStorage;
            Seo = new SeoCoreProduct(_mapper, _db);
        }

        public ISeoCore Seo { get; set; }

        public DbResponse<CatalogDisplayModel> CatalogDetails(int catalogId)
        {
            try
            {
                var data = _db.Catalog.Get(catalogId);

                return new DbResponse<CatalogDisplayModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<CatalogDisplayModel>(false, e.Message);
            }
        }

        public async Task<DbResponse<int>> AddProductAsync(ProductAddModel model, string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0) return new DbResponse<int>(false, "Invalid User");

                model.VendorId = vendorId;
                model.UpdatedOnUtc = DateTime.UtcNow;

                if (!_db.Vendor.IsCatalogExist(vendorId, model.CatalogId))
                    return new DbResponse<int>(false, "Catalog Not Assign");

                if (string.IsNullOrEmpty(model.Name))
                    return new DbResponse<int>(false, "Invalid Data");

                if (_db.Product.IsExistSlugUrl(model.SlugUrl))
                    return new DbResponse<int>(false, "SlugUrl already Exist");

                //add product image
                if (model.ProductImage == null)
                    return new DbResponse<int>(false, "Product Image Required!");

                var count = 1;
                foreach (var img in model.ProductImage)
                {
                    var fileName = FileBuilder.FileNameImage("product-image", img.FileName);
                    var blob = new ProductBlobAddModel
                    {
                        DisplayOrder = count,
                        BlobUrl = await _cloudStorage.UploadFileAsync(img, fileName)
                    };
                    model.Blobs.Add(blob);
                    count++;
                }


                //add attribute
                foreach (var attribute in model.Attributes)
                {
                    foreach (var value in attribute.Values)
                    {
                        if (value.AttributeImage == null) continue;

                        var fileName = FileBuilder.FileNameImage("product-attribute-image", value.AttributeImage.FileName);
                        value.ImageUrl = await _cloudStorage.UploadFileAsync(value.AttributeImage, fileName);
                    }
                }


                _db.Product.Add(model);
                _db.SaveChanges();



                return new DbResponse<int>(true, "Success", _db.Product.Product.ProductId);
            }
            catch (Exception e)
            {
                return new DbResponse<int>(false, e.Message);
            }
        }

        public DbResponse<List<ICatalogVendorModel>> VendorWiseCatalogList(string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                var data = _db.Catalog.VendorWiseList(vendorId);
                return new DbResponse<List<ICatalogVendorModel>>(true, "Success", data.ToList());
            }
            catch (Exception e)
            {
                return new DbResponse<List<ICatalogVendorModel>>(false, e.Message);
            }
        }

        public DbResponse<List<ProductUnpublishedModel>> UnpublishedList(string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0) return new DbResponse<List<ProductUnpublishedModel>>(false, "Invalid User");
                var data = _db.Product.UnpublishedList(vendorId);
                return new DbResponse<List<ProductUnpublishedModel>>(true, "Success", data.ToList());
            }
            catch (Exception e)
            {
                return new DbResponse<List<ProductUnpublishedModel>>(false, e.Message);
            }
        }

        public DbResponse<List<ProductUnpublishedModel>> PublishedList(string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0) return new DbResponse<List<ProductUnpublishedModel>>(false, "Invalid User");
                var data = _db.Product.PublishedList(vendorId);
                return new DbResponse<List<ProductUnpublishedModel>>(true, "Success", data.ToList());
            }
            catch (Exception e)
            {
                return new DbResponse<List<ProductUnpublishedModel>>(false, e.Message);
            }
        }

        public Task<ICollection<BrandModel>> SearchBrandAsync(int catalogId, string key)
        {
            return _db.Brand.SearchAsync(key, catalogId);
        }

        public Task<ICollection<AttributeModel>> SearchAttributeAsync(int catalogId, string key)
        {
            return _db.Attribute.SearchAsync(key, catalogId);
        }

        public Task<ICollection<SpecificationModel>> SearchSpecificationAsync(int catalogId, string key)
        {
            return _db.Specification.SearchAsync(key, catalogId);
        }

        public DbResponse<ProductDetailsModel> Details(string vendorUserName, int productId)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0) return new DbResponse<ProductDetailsModel>(false, "Invalid User");
                if (!_db.Product.IsProductExist(vendorId, productId)) return new DbResponse<ProductDetailsModel>(false, "Product Not Found");


                var data = _db.Product.Details(productId);
                return new DbResponse<ProductDetailsModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductDetailsModel>(false, e.Message);
            }
        }

        public DbResponse<ProductDetailsModel> DetailsBySlugUrl(string slugUrl)
        {
            try
            {
                if (!_db.Product.IsExistSlugUrl(slugUrl))
                    return new DbResponse<ProductDetailsModel>(false, "Product Not Found");

                var productId = _db.Product.ProductIdBySlugUrl(slugUrl);
                var data = _db.Product.Details(productId);
                return new DbResponse<ProductDetailsModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductDetailsModel>(false, e.Message);
            }
        }

        public DbResponse<List<ProductQuantitySetViewModel>> QuantitySetList(int productId, string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0) return new DbResponse<List<ProductQuantitySetViewModel>>(false, "Invalid User");
                if (_db.Product.IsProductExist(vendorId, productId)) return new DbResponse<List<ProductQuantitySetViewModel>>(false, "Product Not Found");

                var data = _db.Product.GetQuantitySetList(productId);

                return new DbResponse<List<ProductQuantitySetViewModel>>(true, "Success", data.ToList());
            }
            catch (Exception e)
            {
                return new DbResponse<List<ProductQuantitySetViewModel>>(false, e.Message);
            }
        }

        public DbResponse<ProductQuantitySetAddReturnModel> QuantityAdd(ProductQuantityAddModel model, string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0) return new DbResponse<ProductQuantitySetAddReturnModel>(false, "Invalid User");

                if (!_db.Product.IsProductExist(vendorId, model.ProductId))
                    return new DbResponse<ProductQuantitySetAddReturnModel>(false, "Product Not Found");

                _db.Product.QuantityAdd(model);
                _db.SaveChanges();

                _db.Product.UpdateMainQuantity(model.ProductId);
                var data = new ProductQuantitySetAddReturnModel
                {
                    StockQuantity = _db.Product.GetStock(model.ProductId),
                    QuantitySet = _db.Product.GetQuantitySetDetailsById(_db.Product.ProductQuantitySet.ProductQuantitySetId)
                };

                return new DbResponse<ProductQuantitySetAddReturnModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductQuantitySetAddReturnModel>(false, e.Message);
            }
        }

        public DbResponse<ProductQuantitySetUpdateReturnModel> QuantityUpdate(ProductQuantityViewModel model)
        {
            try
            {
                _db.Product.QuantityUpdate(model);
                _db.SaveChanges();

                _db.Product.UpdateMainQuantity(model.ProductId);

                var quantitySet = _db.Product.GetQuantitySetById(model.ProductQuantitySetId);

                var data = _mapper.Map<ProductQuantitySetUpdateReturnModel>(quantitySet);
                data.StockQuantity = _db.Product.GetStock(model.ProductId);

                return new DbResponse<ProductQuantitySetUpdateReturnModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductQuantitySetUpdateReturnModel>(false, e.Message);
            }
        }

        public DbResponse PublishedUpdate(int productId, bool published, string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0) return new DbResponse(false, "Invalid User");

                if (!_db.Product.IsProductExist(vendorId, productId))
                    return new DbResponse(false, "Product Not Found");

                _db.Product.PublishedUpdate(productId, published);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<ProductQuantityViewModel> GetQuantitySet(ProductQuantityCheckModel model)
        {
            try
            {
                var data = _db.Product.GetQuantitySet(model);

                if (data == null)
                    return new DbResponse<ProductQuantityViewModel>(false, "Quantity Not Found");

                return new DbResponse<ProductQuantityViewModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductQuantityViewModel>(false, e.Message);
            }
        }

        public DbResponse<ProductQuantityViewModel> GetQuantitySet(ProductQuantityCheckModel model, string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);

                if (vendorId == 0)
                    return new DbResponse<ProductQuantityViewModel>(false, "Invalid User");

                if (!_db.Product.IsProductExist(vendorId, model.ProductId))
                    return new DbResponse<ProductQuantityViewModel>(false, "Product Not Found");

                var data = _db.Product.GetQuantitySet(model);

                if (data == null)
                    return new DbResponse<ProductQuantityViewModel>(false, "Quantity Not Found");

                return new DbResponse<ProductQuantityViewModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductQuantityViewModel>(false, e.Message);
            }
        }

        public DbResponse<PagedResult<ProductListViewModel>> GetTopRated(ProductFilterRequest request)
        {
            try
            {
                var data = _db.Product.GetTopRated(request);
                if (data.Results == null)
                    return new DbResponse<PagedResult<ProductListViewModel>>(false, "No Data found");

                return new DbResponse<PagedResult<ProductListViewModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<PagedResult<ProductListViewModel>>(false, e.Message);
            }
        }

        public DbResponse<PagedResult<ProductListViewModel>> GetFlashDeals(ProductFilterRequest request)
        {
            try
            {
                var data = _db.Product.GetFlashDeals(request);
                if (data.Results == null)
                    return new DbResponse<PagedResult<ProductListViewModel>>(false, "No Data found");

                return new DbResponse<PagedResult<ProductListViewModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<PagedResult<ProductListViewModel>>(false, e.Message);
            }
        }

        public DbResponse<PagedResult<ProductListViewModel>> GetMoreToLove(ProductFilterRequest request)
        {
            try
            {
                var data = _db.Product.GetMoreToLove(request);

                if (data.Results == null)
                    return new DbResponse<PagedResult<ProductListViewModel>>(false, "No Data found");

                return new DbResponse<PagedResult<ProductListViewModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<PagedResult<ProductListViewModel>>(false, e.Message);
            }
        }
    }
}