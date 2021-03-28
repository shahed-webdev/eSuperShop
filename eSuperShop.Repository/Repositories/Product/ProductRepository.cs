using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using Paging.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class ProductRepository : Repository, IProductRepository
    {
        public ProductRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public Product Product { get; set; }
        public ProductQuantitySet ProductQuantitySet { get; set; }

        public void Add(ProductAddModel model)
        {
            Product = _mapper.Map<Product>(model);
            Db.Product.Add(Product);
        }

        public void BlobAddFile(ProductBlobFileChangeModel model)
        {
            var blob = _mapper.Map<ProductBlob>(model);
            Db.ProductBlob.Add(blob);
            Db.SaveChanges();
        }

        public void BlobDeleteFile(ProductBlobFileChangeModel model)
        {
            var blob = Db.ProductBlob.FirstOrDefault(c => c.ProductId == model.ProductId && c.BlobFileName.ToLower() == model.BlobFileName.ToLower());
            Db.ProductBlob.Remove(blob);
            Db.SaveChanges();
        }

        public bool IsExistSlugUrl(string slugUrl)
        {
            return Db.Product.Any(c => c.SlugUrl == slugUrl);
        }

        public bool IsExistBlobFile(int productId, string fileName)
        {
            return Db.ProductBlob.Any(c => c.ProductId == productId && c.BlobFileName.ToLower() == fileName.ToLower());
        }

        public int ProductIdBySlugUrl(string slugUrl)
        {
            return Db.Product.FirstOrDefault(p => p.SlugUrl == slugUrl).ProductId;
        }

        public bool IsExistSlugUrl(string slugUrl, int updateId)
        {
            return Db.Product.Any(c => c.SlugUrl == slugUrl && c.ProductId != updateId);
        }

        public bool IsNull(int id)
        {
            return !Db.Product.Any(c => c.ProductId == id);
        }

        public bool IsRelatedDataExist(int id)
        {
            //Related to order
            throw new System.NotImplementedException();
        }

        public ICollection<ProductUnpublishedModel> UnpublishedList(int vendorId)
        {
            return Db.Product
                .Where(p => p.VendorId == vendorId && !p.Published && !p.IsDeleted)
                .ProjectTo<ProductUnpublishedModel>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.CatalogName).ThenBy(p => p.Name)
                .ToList();
        }

        public ICollection<ProductUnpublishedModel> PublishedList(int vendorId)
        {
            return Db.Product
                .Where(p => p.VendorId == vendorId && p.Published && !p.IsDeleted)
                .ProjectTo<ProductUnpublishedModel>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.CatalogName).ThenBy(p => p.Name)
                .ToList();
        }

        public DataResult<ProductPendingApprovalListModel> PendingApprovalList(DataRequest request)
        {
            return Db.Product
                .Where(p => !p.Published && !p.IsDeleted)
                .ProjectTo<ProductPendingApprovalListModel>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.CreatedOnUtc)
                .ToDataResult(request);
        }

        public ProductDetailsModel Details(int productId)
        {
            //return Db.Product
            //    .Where(p => p.ProductId == productId)
            //    .ProjectTo<ProductDetailsModel>(_mapper.ConfigurationProvider)
            //    .FirstOrDefault();
            var product = Db.Product
                  .Where(p => p.ProductId == productId)
                  .Select(p =>
                      new ProductDetailsModel
                      {
                          ProductId = p.ProductId,
                          VendorInfo = new VendorInfoModel
                          {
                              VendorId = p.VendorId,
                              AuthorizedPerson = p.Vendor.AuthorizedPerson,
                              VerifiedPhone = p.Vendor.VerifiedPhone,
                              StoreName = p.Vendor.StoreName,
                              StoreSlugUrl = p.Vendor.StoreSlugUrl,
                              StoreLogoFileName = p.Vendor.StoreLogoFileName,
                              StoreBannerFileName = p.Vendor.StoreBannerFileName,
                              StoreTagLine = p.Vendor.StoreTagLine,
                              Email = p.Vendor.Email,
                              StoreAddress = p.Vendor.StoreAddress,
                              IsApproved = p.Vendor.IsApproved,
                              GrossSale = p.Vendor.GrossSale,
                              Discount = p.Vendor.Discount,
                              Refund = p.Vendor.Refund,
                              NetSale = p.Vendor.NetSale,
                              Commission = p.Vendor.Commission,
                              Withdraw = p.Vendor.Withdraw,
                              Balance = p.Vendor.Balance,
                              CreatedOnUtc = p.Vendor.CreatedOnUtc,
                              StoreTheme = p.Vendor.StoreTheme,
                              StorePostcode = p.Vendor.StorePostcode,
                              StoreRegionId = p.Vendor.StoreArea.RegionId,
                              StoreAreaId = p.Vendor.StoreAreaId.Value,
                              StoreRegion = p.Vendor.StoreArea.Region.RegionName,
                              StoreArea = p.Vendor.StoreArea.AreaName,
                              NId = p.Vendor.NId,
                              NIdImageFrontFileName = p.Vendor.NIdImageFrontFileName,
                              NIdImageBackFileName = p.Vendor.NIdImageBackFileName,
                              TradeLicenseImageFileName = p.Vendor.TradeLicenseImageFileName,
                              BankAccountTitle = p.Vendor.BankAccountTitle,
                              BankAccountNumber = p.Vendor.BankAccountNumber,
                              BankName = p.Vendor.BankName,
                              BranchName = p.Vendor.BranchName,
                              RoutingNumber = p.Vendor.RoutingNumber,
                              ChequeImageFileName = p.Vendor.ChequeImageFileName,
                              VendorCertificateFileNames = p.Vendor.VendorCertificate.Select(c => c.CertificateImageFileName).ToArray(),
                              MobileBankingType = p.Vendor.MobileBankingType,
                              MobileBankingNumber = p.Vendor.MobileBankingNumber,
                              WarehouseAddress = p.Vendor.WarehouseAddress,
                              WarehousePostcode = p.Vendor.WarehousePostcode,
                              WarehouseAreaId = p.Vendor.WarehouseAreaId.Value,
                              WarehouseRegion = p.Vendor.WarehouseArea.Region.RegionName,
                              WarehouseRegionId = p.Vendor.WarehouseArea.RegionId,
                              WarehouseArea = p.Vendor.WarehouseArea.AreaName,
                              ReturnAddress = p.Vendor.ReturnAddress,
                              ReturnPostcode = p.Vendor.ReturnPostcode,
                              ReturnAreaId = p.Vendor.ReturnAreaId.Value,
                              ReturnRegion = p.Vendor.ReturnArea.Region.RegionName,
                              ReturnRegionId = p.Vendor.ReturnArea.RegionId,
                              ReturnArea = p.Vendor.ReturnArea.AreaName,
                              ReturnPhone = p.Vendor.ReturnPhone,
                              WarehousePhone = p.Vendor.WarehousePhone,
                              ChangedStoreBannerFileName = p.Vendor.ChangedStoreBannerFileName,
                              ChangedStoreLogoFileName = p.Vendor.ChangedStoreLogoFileName,
                              ChangedStoreTagLine = p.Vendor.ChangedStoreTagLine,
                              IsChangedApproved = p.Vendor.IsChangedApproved
                          },
                          CatalogInfo = new CatalogDisplayModel
                          {
                              CatalogId = p.CatalogId,
                              CatalogName = p.Catalog.CatalogName,
                              SlugUrl = p.Catalog.SlugUrl,
                              ImageFileName = p.Catalog.ImageFileName,
                              DisplayOrder = p.Catalog.DisplayOrder
                          },
                          BrandInfo = new BrandModel
                          {
                              BrandId = p.BrandId.Value,
                              Name = p.Brand.Name,
                              LogoFileName = p.Brand.LogoFileName,
                              CreatedBy = p.Brand.CreatedByRegistration.Name
                          },
                          Name = p.Name,
                          SlugUrl = p.SlugUrl,
                          ShortDescription = p.ShortDescription,
                          FullDescription = p.FullDescription,
                          Price = p.Price,
                          OldPrice = p.OldPrice,
                          StockQuantity = p.StockQuantity,
                          Published = p.Published,
                          QuantitySets = p.ProductQuantitySet.Select(q => new ProductQuantitySetViewModel
                          {
                              ProductQuantitySetId = q.ProductQuantitySetId,
                              Quantity = q.Quantity,
                              PriceAdjustment = q.PriceAdjustment,
                              AttributesWithValue = q.ProductQuantitySetAttribute
                                  .Select(a => new ProductQuantitySetAttributeViewModel
                                  {
                                      ProductQuantitySetAttributeId = a.ProductQuantitySetAttributeId,
                                      AttributeId = a.ProductAttributeValue.ProductAttribute.AttributeId,
                                      KeyName = a.ProductAttributeValue.ProductAttribute.Attribute.KeyName,
                                      ProductAttributeValueId = a.ProductAttributeValueId,
                                      Value = a.ProductAttributeValue.Value
                                  }

                          ).ToList()
                          }).ToList(),
                          Blobs = p.ProductBlob.Select(b => new ProductBlobViewModel
                          {
                              ProductBlobId = b.ProductBlobId,
                              ProductId = b.ProductId,
                              BlobFileName = b.BlobFileName,
                              DisplayOrder = b.DisplayOrder
                          }).ToList(),
                          Specifications = p.ProductSpecification.Select(s => new ProductSpecificationViewModel
                          {
                              ProductSpecificationId = s.ProductSpecificationId,
                              ProductId = s.ProductId,
                              SpecificationId = s.SpecificationId,
                              KeyName = s.Specification.KeyName,
                              Value = s.Value
                          }).ToList()
                      }).FirstOrDefault();

            return product;
        }

        public ProductDetailsForSellerModel DetailsForSeller(int productId)
        {
            var product = new ProductDetailsForSellerModel
            {
                ProductInfo = Db.Product
                    .Where(p => p.ProductId == productId)
                    .ProjectTo<ProductInfoSeller>(_mapper.ConfigurationProvider)
                    .FirstOrDefault(),
                BlobFileNames = Db.ProductBlob.Where(p => p.ProductId == productId).Select(b => b.BlobFileName)
                    .ToArray(),
                Specifications = Db.ProductSpecification
                    .Where(p => p.ProductId == productId)
                    .ProjectTo<ProductSpecificationForSellerModel>(_mapper.ConfigurationProvider)
                    .ToList(),
                Attributes = Db.ProductSpecification
                    .Where(p => p.ProductId == productId)
                    .ProjectTo<ProductAttributeSellerViewModel>(_mapper.ConfigurationProvider)
                    .ToList(),
                QuantitySets = Db.ProductSpecification
                    .Where(p => p.ProductId == productId)
                    .ProjectTo<ProductQuantitySetSellerModel>(_mapper.ConfigurationProvider)
                    .ToList()
            };

            return product;
        }

        public ProductDetailsViewModel DetailsView(int productId)
        {
            return Db.Product
                .Where(p => p.ProductId == productId)
                .ProjectTo<ProductDetailsViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public bool IsProductExist(int vendorId, int productId)
        {
            return Db.Product.Any(p => p.ProductId == productId && p.VendorId == vendorId);
        }

        public void QuantityAdd(ProductQuantityAddModel model)
        {
            ProductQuantitySet = _mapper.Map<ProductQuantitySet>(model);
            Db.ProductQuantitySet.Add(ProductQuantitySet);
        }

        public void QuantitySale(int productQuantitySetId, int quantity)
        {
            var productQuantitySet = Db.ProductQuantitySet
                        .FirstOrDefault(p => p.ProductQuantitySetId == productQuantitySetId);

            if (productQuantitySet == null) return;

            productQuantitySet.Quantity -= quantity;
            Db.ProductQuantitySet.Update(productQuantitySet);
        }

        public void QuantityRestock(int productQuantitySetId, int quantity)
        {
            var productQuantitySet = Db.ProductQuantitySet
                .FirstOrDefault(p => p.ProductQuantitySetId == productQuantitySetId);

            if (productQuantitySet == null) return;

            productQuantitySet.Quantity += quantity;
            Db.ProductQuantitySet.Update(productQuantitySet);
        }

        public void QuantityUpdate(ProductQuantityViewModel model)
        {
            var productQuantitySet = Db.ProductQuantitySet.Find(model.ProductQuantitySetId);

            productQuantitySet.Quantity = model.Quantity;
            productQuantitySet.PriceAdjustment = model.PriceAdjustment;
            Db.ProductQuantitySet.Update(productQuantitySet);
        }

        public ProductQuantitySetViewModel GetQuantitySet(ProductQuantityCheckModel model)
        {
            var setList = Db.ProductQuantitySet
                .Include(p => p.ProductQuantitySetAttribute)
                .ThenInclude(a => a.ProductAttributeValue.ProductAttribute.Attribute)
                .Where(p =>
                 p.ProductId == model.ProductId &&
                 p.ProductQuantitySetAttribute.Count == model.ProductAttributeValueIds.Length)
                .ToList();

            if (setList.Count <= 0) return null;

            var set = setList
                  .Where(p => p.ProductQuantitySetAttribute
                      .Select(q => q.ProductAttributeValueId)
                      .All(model.ProductAttributeValueIds.Contains))
                  .Select(p => _mapper.Map<ProductQuantitySetViewModel>(p))
                  .FirstOrDefault();
            return set;
        }

        public ProductQuantityViewModel GetQuantitySetById(int productQuantitySetId)
        {
            return Db.ProductQuantitySet
                .Where(p => p.ProductQuantitySetId == productQuantitySetId)
                .ProjectTo<ProductQuantityViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public int GetQuantityBySetId(int productQuantitySetId)
        {
            return Db.ProductQuantitySet
                .FirstOrDefault(p => p.ProductQuantitySetId == productQuantitySetId)?
                .Quantity ?? 0;
        }

        public ICollection<ProductQuantitySetViewModel> GetQuantitySetList(int productId)
        {
            return Db.ProductQuantitySet
                .Where(p => p.ProductId == productId)
                .ProjectTo<ProductQuantitySetViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public ProductQuantitySetViewModel GetQuantitySetDetailsById(int productQuantitySetId)
        {
            return Db.ProductQuantitySet
                .Where(p => p.ProductQuantitySetId == productQuantitySetId)
                .ProjectTo<ProductQuantitySetViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public void ApprovedByAdmin(ProductApprovedModel model)
        {
            var product = Db.Product.Find(model.ProductId);

            product.BrandId = model.BrandId;
            product.Name = model.Name;
            product.SlugUrl = model.SlugUrl;
            product.ShortDescription = model.ShortDescription;
            product.FullDescription = model.FullDescription;
            product.AdminComment = model.AdminComment;
            product.Published = true;
            product.UpdatedOnUtc = DateTime.UtcNow;

            Db.Product.Update(product);
        }

        public void PublishedUpdate(int productId, bool published)
        {
            var product = Db.Product.Find(productId);
            product.Published = published;
            Db.Product.Update(product);
        }

        public void UpdateMainQuantity(int productId)
        {
            var product = Db.Product.Find(productId);
            var quantity = Db.ProductQuantitySet
                .Where(q => q.ProductId == productId)
                .Sum(q => q.Quantity);
            product.StockQuantity = quantity;
            Db.Product.Update(product);
            Db.SaveChanges();
        }

        public int GetStock(int productId)
        {
            return Db.Product.Find(productId).StockQuantity;
        }

        public PagedResult<ProductListViewModel> GetFlashDeals(ProductFilterRequest request)
        {
            var products = Db.Product
                .Where(p => p.Published && !p.IsDeleted)
                .ProjectTo<ProductListViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.Rating).ThenBy(s => s.RatingBy)
                .GetPaged(request.Page, request.PageSize);
            return products;
        }

        public PagedResult<ProductListViewModel> GetTopRated(ProductFilterRequest request)
        {
            var products = Db.Product
                .Where(p => p.Published && !p.IsDeleted)
                .ProjectTo<ProductListViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.Rating).ThenBy(s => s.RatingBy)
                .GetPaged(request.Page, request.PageSize);
            return products;
        }

        public PagedResult<ProductListViewModel> GetMoreToLove(ProductFilterRequest request)
        {
            var products = Db.Product
                .Where(p => p.Published && !p.IsDeleted)
                .ProjectTo<ProductListViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.Rating).ThenBy(s => s.RatingBy)
                .GetPaged(request.Page, request.PageSize);
            return products;
        }

        public PagedResult<ProductListViewModel> GetCatalogWiseList(List<int> catalogIds, ProductFilterRequest request)
        {
            var products = Db.Product
                .Where(p => p.Published && !p.IsDeleted && catalogIds.Contains(p.CatalogId))
                .ProjectTo<ProductListViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.Rating).ThenBy(s => s.RatingBy)
                .GetPaged(request.Page, request.PageSize);
            return products;
        }

        public PagedResult<ProductListViewModel> GetVendorWiseList(int vendorId, ProductFilterRequest request)
        {
            var products = Db.Product
                .Where(p => p.Published && !p.IsDeleted && p.VendorId == vendorId)
                .ProjectTo<ProductListViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.Rating).ThenBy(s => s.RatingBy)
                .GetPaged(request.Page, request.PageSize);
            return products;
        }

        public SeoModel GetSeo(int id)
        {
            return Db.Product
                .Where(c => c.ProductId == id)
                .Select(c => c.Seo)
                .ProjectTo<SeoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public void SeoDelete(int id)
        {
            var seo = Db.Product.Include(c => c.Seo).FirstOrDefault(c => c.ProductId == id).Seo;
            Db.Seo.Remove(seo);
        }

        public bool IsSeoExist(int id)
        {
            return Db.Product.Any(c => c.ProductId == id && c.SeoId != null);
        }

        public void PostSeo(SeoAddModel model)
        {
            var product = Db.Product.Include(c => c.Seo).FirstOrDefault(c => c.ProductId == model.AssignTableId);

            if (product == null) return;

            if (product.Seo == null)
            {
                var seo = _mapper.Map<Seo>(model);
                product.Seo = seo;
            }
            else
            {
                product.Seo.MetaTitle = model.MetaTitle;
                product.Seo.MetaDescription = model.MetaDescription;
                product.Seo.MetaKeywords = model.MetaKeywords;
            }

            Db.Product.Update(product);
        }


    }
}