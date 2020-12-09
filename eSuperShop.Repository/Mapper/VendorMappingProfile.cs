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
            CreateMap<Vendor, VendorModel>().ReverseMap();
            CreateMap<Vendor, VendorInfoModel>().ReverseMap();
            CreateMap<Vendor, VendorStoreInfoUpdateModel>().ReverseMap();


            CreateMap<VendorStoreSlider, VendorStoreSliderViewModel>().ReverseMap();
            //Vendor store Mapping
            CreateMap<Vendor, StoreViewModel>()
                .ForMember(d => d.ProductImageUrls,
                    opt => opt.MapFrom(c => c.Product.Select(p => p.ProductBlob.First().BlobUrl).ToArray()))
                .ForMember(d => d.RatingBy, opt => opt.MapFrom(c => c.VendorReview.Count()))
                .ForMember(d => d.Rating,
                    opt => opt.MapFrom(c => (double)c.VendorReview.Sum(r => r.Rating) / (double)c.VendorReview.Count()))
                .ReverseMap();

            CreateMap<Vendor, StoreThemeViewModel>()
                .ForMember(d => d.RatingBy, opt => opt.MapFrom(c => c.VendorReview.Count()))
                .ForMember(d => d.Rating,
                    opt => opt.MapFrom(c => (double)c.VendorReview.Sum(r => r.Rating) / (double)c.VendorReview.Count()))
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
        }
    }
}