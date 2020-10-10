using AutoMapper;
using CloudStorage;
using eSuperShop.Repository;
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
        }

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

        public async Task<DbResponse> AddProductAsync(ProductAddModel model, string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0) return new DbResponse(false, "Invalid User");

                model.VendorId = vendorId;
                model.UpdatedOnUtc = DateTime.UtcNow;

                if (!_db.Vendor.IsCatalogExist(vendorId, model.CatalogId))
                    return new DbResponse(false, "Catalog Not Assign");

                if (string.IsNullOrEmpty(model.Name))
                    return new DbResponse(false, "Invalid Data");

                if (_db.Product.IsExistSlugUrl(model.SlugUrl))
                    return new DbResponse(false, "SlugUrl already Exist");

                //add product image
                if (model.ProductImage == null)
                    return new DbResponse(false, "Product Image Required!");

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



                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
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
                if (_db.Product.IsProductExist(vendorId, productId)) return new DbResponse<ProductDetailsModel>(false, "Product Not Found");


                var data = _db.Product.Details(productId);
                return new DbResponse<ProductDetailsModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductDetailsModel>(false, e.Message);
            }
        }
    }
}