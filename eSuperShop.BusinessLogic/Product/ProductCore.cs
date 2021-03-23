using AutoMapper;
using CloudStorage;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
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
                        BlobFileName = await _cloudStorage.UploadFileAsync(img, fileName)
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
                        value.ImageFileName = await _cloudStorage.UploadFileAsync(value.AttributeImage, fileName);
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

        public DataResult<ProductPendingApprovalListModel> PendingApprovalList(DataRequest request)
        {
            return _db.Product.PendingApprovalList(request);
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

        public DbResponse<ProductDetailsModel> Details(int productId)
        {
            try
            {
                if (!_db.Product.IsNull(productId))
                    return new DbResponse<ProductDetailsModel>(false, "Product Not Found");


                var data = _db.Product.Details(productId);
                return new DbResponse<ProductDetailsModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductDetailsModel>(false, e.Message);
            }
        }

        public DbResponse<ProductDetailsViewModel> DetailsBySlugUrl(string slugUrl)
        {
            try
            {
                if (!_db.Product.IsExistSlugUrl(slugUrl))
                    return new DbResponse<ProductDetailsViewModel>(false, "Product Not Found");

                var productId = _db.Product.ProductIdBySlugUrl(slugUrl);

                var data = _db.Product.DetailsView(productId);

                data.AverageReview = _db.ProductReview.AverageReview(productId);
                data.CatalogBreadcrumb = _db.Catalog.BreadcrumbById(data.CatalogId);
                //data.Faqs = _db.ProductFaq.ProductWiseList(new ProductReviewFilerRequest
                //{
                //    Page = 1,
                //    PageSize = 10,
                //    ProductId = data.ProductId
                //}).Results;
                data.TotalFaqs = data.Faqs.Count();
                data.RecommendedProducts = _db.Product.GetCatalogWiseList(new List<int> { data.CatalogId },
                    new ProductFilterRequest { Page = 1, PageSize = 3 }).Results;
                data.VendorMoreProducts = _db.Product.GetVendorWiseList(data.VendorId,
                    new ProductFilterRequest { Page = 1, PageSize = 3 }).Results;
                data.Specifications = _db.Specification.ProductWiseList(data.ProductId);
                return new DbResponse<ProductDetailsViewModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductDetailsViewModel>(false, e.Message);
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

        public DbResponse ApprovedByAdmin(ProductApprovedModel model)
        {
            try
            {
                if (!_db.Product.IsNull(model.ProductId))
                    return new DbResponse(false, "Product Not Found");

                if (string.IsNullOrEmpty(model.Name))
                    return new DbResponse(false, "Invalid Data");

                if (_db.Product.IsExistSlugUrl(model.SlugUrl, model.ProductId))
                    return new DbResponse(false, "SlugUrl already Exist");
                _db.Product.ApprovedByAdmin(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<ProductQuantitySetViewModel> GetQuantitySet(ProductQuantityCheckModel model)
        {
            try
            {
                var data = _db.Product.GetQuantitySet(model);

                if (data == null)
                    return new DbResponse<ProductQuantitySetViewModel>(false, "Quantity Not Found");

                return new DbResponse<ProductQuantitySetViewModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductQuantitySetViewModel>(false, e.Message);
            }
        }

        public DbResponse<int> GetQuantityBySetId(int productQuantitySetId)
        {
            try
            {
                var data = _db.Product.GetQuantityBySetId(productQuantitySetId);


                return new DbResponse<int>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<int>(false, e.Message);
            }
        }

        public DbResponse<ProductQuantitySetViewModel> GetQuantitySet(ProductQuantityCheckModel model, string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);

                if (vendorId == 0)
                    return new DbResponse<ProductQuantitySetViewModel>(false, "Invalid User");

                if (!_db.Product.IsProductExist(vendorId, model.ProductId))
                    return new DbResponse<ProductQuantitySetViewModel>(false, "Product Not Found");

                var data = _db.Product.GetQuantitySet(model);

                if (data == null)
                    return new DbResponse<ProductQuantitySetViewModel>(false, "Quantity Not Found");

                return new DbResponse<ProductQuantitySetViewModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductQuantitySetViewModel>(false, e.Message);
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

        public DbResponse AddReview(ProductReviewAddModel model)
        {
            try
            {


                if (!_db.ProductReview.IsReviewExist(model.ProductId, model.CustomerId))
                    return new DbResponse(false, "Review already added");


                _db.ProductReview.Add(model);
                _db.SaveChanges();



                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<PagedResult<ProductReviewViewModel>> ReviewList(ProductReviewFilerRequest request)
        {
            try
            {
                var data = _db.ProductReview.ProductWiseList(request);

                if (data.Results == null)
                    return new DbResponse<PagedResult<ProductReviewViewModel>>(false, "No Data found");

                return new DbResponse<PagedResult<ProductReviewViewModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<PagedResult<ProductReviewViewModel>>(false, e.Message);
            }
        }

        public DbResponse<ProductReviewAverageModel> AverageReview(int productId)
        {
            try
            {
                var data = _db.ProductReview.AverageReview(productId);


                return new DbResponse<ProductReviewAverageModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<ProductReviewAverageModel>(false, e.Message);
            }
        }

        public DbResponse FaqAdd(ProductFaqAddModel model, string customerUserName)
        {
            try
            {
                var customerId = _db.Registration.CustomerIdByUserName(customerUserName);

                if (customerId == 0)
                    return new DbResponse(false, "Invalid User");
                model.CustomerId = customerId;

                _db.ProductFaq.Add(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse FaqAnswerAdd(ProductFaqAnswerModel model)
        {
            try
            {
                _db.ProductFaq.Answer(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<PagedResult<FaqProductWiseViewModel>> FaqProductWiseList(ProductReviewFilerRequest request)
        {
            try
            {
                var data = _db.ProductFaq.ProductWiseList(request);

                if (data.Results == null)
                    return new DbResponse<PagedResult<FaqProductWiseViewModel>>(false, "No Data found");

                return new DbResponse<PagedResult<FaqProductWiseViewModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<PagedResult<FaqProductWiseViewModel>>(false, e.Message);
            }
        }

        public DbResponse<PagedResult<FaqCustomerWiseViewModel>> FaqCustomerWiseList(ProductReviewFilerRequest request)
        {
            try
            {
                var data = _db.ProductFaq.CustomerWiseList(request);

                if (data.Results == null)
                    return new DbResponse<PagedResult<FaqCustomerWiseViewModel>>(false, "No Data found");

                return new DbResponse<PagedResult<FaqCustomerWiseViewModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<PagedResult<FaqCustomerWiseViewModel>>(false, e.Message);
            }
        }

        public DbResponse<PagedResult<FaqVendorWiseViewModel>> FaqVendorWiseList(ProductReviewFilerRequest request)
        {
            try
            {
                var data = _db.ProductFaq.VendorWiseList(request);

                if (data.Results == null)
                    return new DbResponse<PagedResult<FaqVendorWiseViewModel>>(false, "No Data found");

                return new DbResponse<PagedResult<FaqVendorWiseViewModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<PagedResult<FaqVendorWiseViewModel>>(false, e.Message);
            }
        }
    }
}