using AutoMapper;
using eSuperShop.Data;
using System.Linq;

namespace eSuperShop.Repository
{
    public class VendorMappingProfile : Profile
    {
        public VendorMappingProfile()
        {

            //Vendor Mapping
            CreateMap<Vendor, VendorAddModel>().ReverseMap();
            CreateMap<Vendor, VendorModel>()
                .ForMember(d => d.StoreArea, opt => opt.MapFrom(c => c.StoreArea.AreaName))
                .ForMember(d => d.StoreRegion, opt => opt.MapFrom(c => c.StoreArea.Region.RegionName))
                .ReverseMap();
            CreateMap<Vendor, VendorInfoModel>()
                .ForMember(d => d.StoreLogoUrl, opt => opt.MapFrom(c => $"thumb_{c.StoreLogoUrl}"))
                .ForMember(d => d.StoreBannerUrl, opt => opt.MapFrom(c => $"thumb_{c.StoreBannerUrl}"))
                .ForMember(d => d.NIdImageFrontUrl, opt => opt.MapFrom(c => $"thumb_{c.NIdImageFrontUrl}"))
                .ForMember(d => d.NIdImageBackUrl, opt => opt.MapFrom(c => $"thumb_{c.NIdImageBackUrl}"))
                .ForMember(d => d.StoreBannerUrl, opt => opt.MapFrom(c => $"thumb_{c.StoreBannerUrl}"))
                .ForMember(d => d.StoreArea, opt => opt.MapFrom(c => c.StoreArea.AreaName))
                .ForMember(d => d.StoreRegion, opt => opt.MapFrom(c => c.StoreArea.Region.RegionName))
                .ForMember(d => d.WarehouseArea, opt => opt.MapFrom(c => c.WarehouseArea.AreaName))
                .ForMember(d => d.WarehouseRegion, opt => opt.MapFrom(c => c.WarehouseArea.Region.RegionName))
                .ForMember(d => d.ReturnArea, opt => opt.MapFrom(c => c.ReturnArea.AreaName))
                .ForMember(d => d.ReturnRegion, opt => opt.MapFrom(c => c.ReturnArea.Region.RegionName))
                .ForMember(d => d.VendorCertificateUrl, opt => opt.MapFrom(c => c.VendorCertificate.Select(c => $"thumb_{c.CertificateImageUrl}").ToArray()))

                .ReverseMap();
            CreateMap<Vendor, VendorInfoUpdateModel>().ReverseMap();


            CreateMap<VendorStoreSlider, VendorStoreSliderViewModel>().ReverseMap();
            //Vendor store Mapping
            CreateMap<Vendor, StoreViewModel>()
                .ForMember(d => d.ProductImageUrls,
                    opt => opt.MapFrom(c => c.Product.Where(p => p.Published).Select(p => p.ProductBlob.First().BlobUrl).ToArray()))
                .ForMember(d => d.RatingBy, opt => opt.MapFrom(c => c.VendorReview.Count()))
                .ForMember(d => d.Rating,
                    opt => opt.MapFrom(c => (double)c.VendorReview.Sum(r => r.Rating) / (double)c.VendorReview.Count()))
                .ReverseMap();

            CreateMap<Vendor, StoreThemeViewModel>()
                .ForMember(d => d.RatingBy, opt => opt.MapFrom(c => c.VendorReview.Count()))
                .ForMember(d => d.Rating,
                    opt => opt.MapFrom(c => (double)c.VendorReview.Sum(r => r.Rating) / (double)c.VendorReview.Count()))
                .ReverseMap();

            //vendor VendorProductCategory Mapping
            CreateMap<VendorProductCategory, StoreProductViewModel>()
                 .ForMember(d => d.Products, opt => opt.MapFrom(c => c.VendorProductCategoryList.Select(p => p.Product)))
                .ReverseMap();

            //Vendor Slider Mapping
            CreateMap<VendorStoreSlider, VendorSliderModel>().ReverseMap();
            CreateMap<VendorStoreSlider, VendorSliderSlideModel>().ReverseMap();

            //Vendor ProductCategory Mapping
            CreateMap<VendorProductCategory, VendorProductCategoryAddModel>().ReverseMap();
            CreateMap<VendorProductCategory, VendorProductCategoryModel>().ReverseMap();
            CreateMap<VendorProductCategory, VendorProductCategoryDisplayModel>().ReverseMap();
            CreateMap<VendorProductCategory, VendorProductCategoryUpdateModel>().ReverseMap();


            //Vendor ProductCategory Assign Mapping
            CreateMap<VendorProductCategoryList, VendorProductCategoryAssignModel>().ReverseMap();


            CreateMap<VendorDataChangeApprovedModel, Vendor>().ReverseMap();


        }
    }
}