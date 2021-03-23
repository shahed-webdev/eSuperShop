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
            return Db.ProductBlob.Any(c => c.ProductId == productId && c.BlobFileName.ToLower()==fileName.ToLower());
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
            return Db.Product
                .Where(p => p.ProductId == productId)
                .ProjectTo<ProductDetailsModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
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