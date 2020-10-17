using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class ProductRepository : Repository, IProductRepository
    {
        public ProductRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public void Add(ProductAddModel model)
        {
            //var product = new Product
            //{
            //    VendorId = model.VendorId,
            //    CatalogId = model.CatalogId,
            //    BrandId = model.BrandId,
            //    Name = model.Name,
            //    SlugUrl = model.SlugUrl,
            //    ShortDescription = model.ShortDescription,
            //    FullDescription = model.FullDescription,
            //    Price = model.Price,
            //    OldPrice = model.OldPrice,
            //    UpdatedOnUtc = model.UpdatedOnUtc,
            //    ProductAttribute = model.Attributes.Select(a => new ProductAttribute
            //    {
            //        AttributeId = a.AttributeId,
            //        DisplayType = a.DisplayType,
            //        ProductAttributeValue = a.Values.Select(v => new ProductAttributeValue
            //        {
            //            Value = v.Value,
            //            ImageUrl = v.ImageUrl
            //        }).ToList()
            //    }).ToList(),
            //    ProductQuantitySet = null,
            //    ProductSpecification = model.Specifications.Select(s => new ProductSpecification
            //    {
            //        SpecificationId = s.SpecificationId,
            //        Value = s.Value
            //    }).ToList(),
            //    ProductBlob = model.Blobs.Select(b => new ProductBlob
            //    {
            //        BlobUrl = b.BlobUrl,
            //        DisplayOrder = b.DisplayOrder,
            //    }).ToList()
            //};

            var product = _mapper.Map<Product>(model);
            Db.Product.Add(product);
        }

        public bool IsExistSlugUrl(string slugUrl)
        {
            return Db.Product.Any(c => c.SlugUrl == slugUrl);
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
                .Where(p => p.VendorId == vendorId && !p.Published)
                .ProjectTo<ProductUnpublishedModel>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.CatalogName).ThenBy(p => p.Name)
                .ToList();
        }

        public ICollection<ProductUnpublishedModel> PublishedList(int vendorId)
        {
            return Db.Product
                .Where(p => p.VendorId == vendorId && p.Published)
                .ProjectTo<ProductUnpublishedModel>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.CatalogName).ThenBy(p => p.Name)
                .ToList();
        }

        public ProductDetailsModel Details(int productId)
        {
            return Db.Product
                .Where(p => p.ProductId == productId)
                .ProjectTo<ProductDetailsModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public bool IsProductExist(int vendorId, int productId)
        {
            return Db.Product.Any(p => p.ProductId == productId && p.VendorId == vendorId);
        }

        public void QuantityAdd(ProductQuantityAddModel model)
        {
            var productQuantitySetAdd = _mapper.Map<ProductQuantitySet>(model);
            Db.ProductQuantitySet.Add(productQuantitySetAdd);
        }

        public void QuantityUpdate(ProductQuantityViewModel model)
        {
            var productQuantitySet = Db.ProductQuantitySet.Find(model.ProductQuantitySetId);

            productQuantitySet.Quantity = model.Quantity;
            productQuantitySet.PriceAdjustment = model.PriceAdjustment;
            Db.ProductQuantitySet.Update(productQuantitySet);
        }

        public ProductQuantityViewModel GetQuantitySet(ProductQuantityCheckModel model)
        {
            var setList = Db.ProductQuantitySet
                .Include(p => p.ProductQuantitySetAttribute)
                .Where(p =>
                 p.ProductId == model.ProductId &&
                 p.ProductQuantitySetAttribute.Count == model.ProductAttributeValueIds.Length)
                .ToList();

            if (setList.Count <= 0) return null;

            var set = setList
                  .Where(p => p.ProductQuantitySetAttribute
                      .Select(q => q.ProductAttributeValueId)
                      .All(model.ProductAttributeValueIds.Contains))
                  .Select(p => new ProductQuantityViewModel
                  {
                      ProductQuantitySetId = p.ProductQuantitySetId,
                      Quantity = p.Quantity,
                      PriceAdjustment = p.PriceAdjustment
                  }).FirstOrDefault();
            return set;
        }

        public ICollection<ProductQuantitySetViewModel> GetQuantitySetList(int productId)
        {
            return Db.ProductQuantitySet
                .Where(p => p.ProductId == productId)
                .ProjectTo<ProductQuantitySetViewModel>(_mapper.ConfigurationProvider)
                .ToList();
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