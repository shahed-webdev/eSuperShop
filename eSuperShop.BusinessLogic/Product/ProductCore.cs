using AutoMapper;
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

        public ProductCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public DbResponse AddProduct(ProductAddModel model, string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0) return new DbResponse(false, "Invalid User");

                model.VendorId = vendorId;

                if (string.IsNullOrEmpty(model.Name))
                    return new DbResponse(false, "Invalid Data");

                if (_db.Product.IsExistSlugUrl(model.SlugUrl))
                    return new DbResponse(false, "SlugUrl already Exist");

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
    }
}