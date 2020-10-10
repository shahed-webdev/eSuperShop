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
                //.Include(p => p.Catalog)
                //.Include(p => p.Brand)
                .Where(p => p.VendorId == vendorId && !p.Published)
                .ProjectTo<ProductUnpublishedModel>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.CatalogName).ThenBy(p => p.Name)
                .ToList();
        }
    }
}