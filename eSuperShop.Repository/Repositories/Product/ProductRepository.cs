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
            var productQuantitySet = Db.ProductQuantitySet.Where(p =>
                p.ProductId == model.ProductId && p.ProductQuantitySetAttribute.Select(q => q.ProductAttributeValueId)
                    .All(model.ProductQuantitySetAttribute.Select(s => s.ProductAttributeValueId).Contains)).ToList();
            if (productQuantitySet == null)
            {
                var productQuantitySetAdd = _mapper.Map<ProductQuantitySet>(model);
                Db.ProductQuantitySet.Add(productQuantitySetAdd);
            }

        }

        public ProductQuantityViewModel GetQuantitySet(ProductQuantityCheckModel model)
        {
            return Db.ProductQuantitySet.Where(p =>
                p.ProductId == model.ProductId &&
                p.ProductQuantitySetAttribute.Select(q => q.ProductAttributeValueId)
                    .All(model.ProductAttributeValueIds.Contains) &&
                p.ProductQuantitySetAttribute.Count == model.ProductAttributeValueIds.Length)
                .ProjectTo<ProductQuantityViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public ICollection<ProductQuantitySetViewModel> GetQuantitySetList(int productId)
        {
            return Db.ProductQuantitySet
                .Where(p => p.ProductId == productId)
                .ProjectTo<ProductQuantitySetViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}