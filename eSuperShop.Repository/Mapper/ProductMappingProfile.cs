﻿using AutoMapper;
using eSuperShop.Data;
using System.Linq;

namespace eSuperShop.Repository
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            //Product Add
            CreateMap<Product, ProductAddModel>()
                .ForMember(d => d.Attributes, opt => opt.MapFrom(c => c.ProductAttribute))
                .ForMember(d => d.Blobs, opt => opt.MapFrom(c => c.ProductBlob))
                .ForMember(d => d.Specifications, opt => opt.MapFrom(c => c.ProductSpecification))
                .ReverseMap();
            CreateMap<ProductBlob, ProductBlobFileChangeModel>().ReverseMap();
            CreateMap<ProductBlob, ProductBlobAddModel>().ReverseMap();
            CreateMap<ProductSpecification, ProductSpecificationAddModel>().ReverseMap();
            CreateMap<ProductAttributeValue, ProductAttributeValueAddModel>().ReverseMap();
            CreateMap<ProductAttribute, ProductAttributeAddModel>()
                .ForMember(d => d.Values, opt => opt.MapFrom(c => c.ProductAttributeValue))
                .ReverseMap();



            //Vendor Product show
            CreateMap<Product, ProductUnpublishedModel>()
                .ForMember(d => d.BrandName, opt => opt.MapFrom(c => c.Brand.Name))
                .ForMember(d => d.CatalogName, opt => opt.MapFrom(c => c.Catalog.CatalogName));


            //Vendor Product show
            CreateMap<Product, ProductListVendorCategoryWiseModel>()
                .ForMember(d => d.BrandName, opt => opt.MapFrom(c => c.Brand.Name))
                .ForMember(d => d.CatalogName, opt => opt.MapFrom(c => c.Catalog.CatalogName));



            CreateMap<ProductBlob, ProductBlobViewModel>().ReverseMap();
            CreateMap<ProductSpecification, ProductSpecificationViewModel>()
                .ForMember(d => d.KeyName, opt => opt.MapFrom(c => c.Specification.KeyName))
                .ReverseMap();
            CreateMap<ProductAttributeValue, ProductAttributeValueViewModel>().ReverseMap();
            CreateMap<ProductAttribute, ProductAttributeViewModel>()
                .ForMember(d => d.Values, opt => opt.MapFrom(c => c.ProductAttributeValue))
                .ForMember(d => d.KeyName, opt => opt.MapFrom(c => c.Attribute.KeyName))
                .ReverseMap();
            CreateMap<Product, ProductDetailsModel>()
                .ForMember(d => d.Attributes,
                    opt => opt.MapFrom(c => c.ProductAttribute.OrderBy(p => p.Attribute.KeyName)))
                .ForMember(d => d.Blobs, opt => opt.MapFrom(c => c.ProductBlob))
                .ForMember(d => d.Specifications, opt => opt.MapFrom(c => c.ProductSpecification))
                .ForMember(d => d.BrandInfo, opt => opt.MapFrom(c => c.Brand))
                .ForMember(d => d.CatalogInfo, opt => opt.MapFrom(c => c.Catalog))
                .ForMember(d => d.VendorInfo, opt => opt.MapFrom(c => c.Vendor))
                .ForMember(d => d.QuantitySets, opt => opt.MapFrom(c => c.ProductQuantitySet));

            CreateMap<Product, ProductInfoSeller>()
                .ForMember(d => d.BrandName, opt => opt.MapFrom(c => c.Brand.Name))
                .ForMember(d => d.CatalogName, opt => opt.MapFrom(c => c.Catalog.CatalogName))
                ;
            CreateMap<ProductSpecification, ProductSpecificationForSellerModel>()
                .ForMember(d => d.KeyName, opt => opt.MapFrom(c => c.Specification.KeyName))
                ;

            CreateMap<ProductAttribute, ProductAttributeSellerViewModel>()
                .ForMember(d => d.KeyName, opt => opt.MapFrom(c => c.Attribute.KeyName))
                .ForMember(d => d.Values, opt => opt.MapFrom(c => c.ProductAttributeValue.Select(v => new ProductAttributeSellerValueViewModel
                {
                    ProductAttributeValueId = v.ProductAttributeValueId,
                    Value = v.Value
                })));

            CreateMap<ProductQuantitySet, ProductQuantitySetSellerModel>()
                .ForMember(d => d.Values, opt => opt.MapFrom(c => c.ProductQuantitySetAttribute.Select(v => new ProductQuantitySetValueSellerModel
                {
                    KeyName = v.ProductAttributeValue.ProductAttribute.Attribute.KeyName,
                    Value = v.ProductAttributeValue.Value
                })));

            CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(d => d.Attributes,
                    opt => opt.MapFrom(c => c.ProductAttribute.OrderBy(p => p.Attribute.KeyName)))
                .ForMember(d => d.Blobs, opt => opt.MapFrom(c => c.ProductBlob.Select(b => b.BlobFileName).ToArray()))
                .ForMember(d => d.Faqs,
                    opt => opt.MapFrom(c =>
                        c.ProductFaq.Where(f => f.IsVisible && !string.IsNullOrEmpty(f.Answer))
                            .OrderByDescending(f => f.AnswerOnUtc)))
                .ForMember(d => d.Reviews,
                    opt => opt.MapFrom(c => c.ProductReview.OrderByDescending(r => r.ReviewedOnUtc)))
                .ForMember(d => d.StoreName, opt => opt.MapFrom(c => c.Vendor.StoreName))
                .ForMember(d => d.StoreSlugUrl, opt => opt.MapFrom(c => c.Vendor.StoreSlugUrl))
                .ForMember(d => d.ShippingDetails, opt => opt.MapFrom(c => c.Catalog))
                ;
            //.ForMember(d => d.Specifications, opt => opt.MapFrom(c => c.ProductSpecification));





            //Vendor Product Quantity show Mapping

            CreateMap<ProductQuantitySetAttribute, ProductQuantitySetAttributeModel>().ReverseMap();
            CreateMap<ProductQuantitySetAttribute, QuantitySetAttributeValueModel>()
                .ForMember(d => d.KeyName,
                    opt => opt.MapFrom(c => c.ProductAttributeValue.ProductAttribute.Attribute.KeyName))
                .ForMember(d => d.Value,
                    opt => opt.MapFrom(c => c.ProductAttributeValue.Value))
                .ReverseMap();
            CreateMap<ProductQuantitySet, ProductQuantityAddModel>().ReverseMap();


            //Product Quantity Set Mapping
            CreateMap<ProductQuantitySet, ProductQuantityViewModel>().ReverseMap();
            CreateMap<ProductQuantitySet, ProductQuantitySetUpdateReturnModel>().ReverseMap();
            CreateMap<ProductQuantityViewModel, ProductQuantitySetUpdateReturnModel>().ReverseMap();
            CreateMap<ProductQuantitySet, ProductQuantitySetViewModel>()
                .ForMember(d => d.AttributesWithValue,
                    opt => opt.MapFrom(c =>
                        c.ProductQuantitySetAttribute.OrderBy(p =>
                            p.ProductAttributeValue.ProductAttribute.Attribute.KeyName))).ReverseMap();

            CreateMap<ProductQuantitySetAttribute, ProductQuantitySetAttributeViewModel>()
                .ForMember(d => d.AttributeId,
                    opt => opt.MapFrom(c => c.ProductAttributeValue.ProductAttribute.AttributeId))
                .ForMember(d => d.KeyName,
                    opt => opt.MapFrom(c => c.ProductAttributeValue.ProductAttribute.Attribute.KeyName))
                .ForMember(d => d.Value, opt => opt.MapFrom(c => c.ProductAttributeValue.Value)).ReverseMap();

            //Product Show Mapping
            CreateMap<Product, ProductListViewModel>()
                .ForMember(d => d.ImageFileName, opt => opt.MapFrom(c => c.ProductBlob.FirstOrDefault().BlobFileName))
                .ForMember(d => d.RatingBy, opt => opt.MapFrom(c => c.ProductReview.Count()))
                .ForMember(d => d.Rating, opt => opt.MapFrom(c => (double)c.ProductReview.Sum(r => r.Rating) / (double)c.ProductReview.Count()))
                .ForMember(d => d.StoreName, opt => opt.MapFrom(c => c.Vendor.StoreName))
                .ReverseMap();

            CreateMap<Product, ProductShortInfo>()
                .ForMember(d => d.ImageFileName, opt => opt.MapFrom(c => c.ProductBlob.FirstOrDefault().BlobFileName))
                .ReverseMap();

            // ProductReview Mapping
            CreateMap<ProductReview, ProductReviewAddModel>().ReverseMap();
            CreateMap<ProductReview, ProductReviewEditModel>().ReverseMap();
            CreateMap<ProductReview, ProductReviewViewModel>()
                .ForMember(d => d.CustomerUserName, opt => opt.MapFrom(c => c.Customer.Registration.UserName))
                .ReverseMap();

            //Product Faq Mapping
            CreateMap<ProductFaq, ProductFaqAddModel>().ReverseMap();
            CreateMap<ProductFaq, ProductFaqAnswerModel>()
                .ForMember(d => d.CustomerUserName, opt => opt.MapFrom(c => c.Customer.Registration.UserName))
                .ReverseMap();
            CreateMap<ProductFaq, FaqCustomerWiseViewModel>().ReverseMap();
            CreateMap<ProductFaq, FaqProductWiseViewModel>()
                .ForMember(d => d.CustomerUserName, opt => opt.MapFrom(c => c.Customer.Registration.UserName))
                .ReverseMap();
            CreateMap<ProductFaq, FaqVendorWiseViewModel>()
                .ForMember(d => d.CustomerUserName, opt => opt.MapFrom(c => c.Customer.Registration.UserName))
                .ReverseMap();


            //Product Lsit
            CreateMap<Product, ProductPendingApprovalListModel>()
                .ForMember(d => d.ImageFileName, opt => opt.MapFrom(c => c.ProductBlob.FirstOrDefault().BlobFileName))
                .ForMember(d => d.AuthorizedPerson, opt => opt.MapFrom(c => c.Vendor.AuthorizedPerson))
                .ForMember(d => d.Email, opt => opt.MapFrom(c => c.Vendor.Email))
                .ForMember(d => d.StoreName, opt => opt.MapFrom(c => c.Vendor.StoreName))
                .ForMember(d => d.VerifiedPhone, opt => opt.MapFrom(c => c.Vendor.VerifiedPhone))
                .ForMember(d => d.ProductName, opt => opt.MapFrom(c => c.Name))
                .ForMember(d => d.ProductSlugUrl, opt => opt.MapFrom(c => c.SlugUrl))
                .ForMember(d => d.CatalogName, opt => opt.MapFrom(c => c.Catalog.CatalogName))
                ;

            CreateMap<Product, ProductDetailsApprovedModel>()
                .ForMember(d => d.BrandName, opt => opt.MapFrom(c => c.Brand.Name))
                .ForMember(d => d.CatalogName, opt => opt.MapFrom(c => c.Catalog.CatalogName))
                .ForMember(d => d.BlobFileNames, opt => opt.MapFrom(c => c.ProductBlob.Select(b => b.BlobFileName).ToArray()))
                ;
        }
    }
}