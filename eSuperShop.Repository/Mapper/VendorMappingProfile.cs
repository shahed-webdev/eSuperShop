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
                .ForMember(d => d.StoreArea, opt => opt.MapFrom(c => c.StoreArea.AreaName))
                .ForMember(d => d.StoreRegionId, opt => opt.MapFrom(c => c.StoreArea.RegionId))
                .ForMember(d => d.StoreRegion, opt => opt.MapFrom(c => c.StoreArea.Region.RegionName))
                .ForMember(d => d.WarehouseArea, opt => opt.MapFrom(c => c.WarehouseArea.AreaName))
                .ForMember(d => d.WarehouseRegionId, opt => opt.MapFrom(c => c.WarehouseArea.RegionId))

                .ForMember(d => d.WarehouseRegion, opt => opt.MapFrom(c => c.WarehouseArea.Region.RegionName))
                .ForMember(d => d.ReturnArea, opt => opt.MapFrom(c => c.ReturnArea.AreaName))
                .ForMember(d => d.ReturnRegionId, opt => opt.MapFrom(c => c.ReturnArea.RegionId))
                .ForMember(d => d.ReturnRegion, opt => opt.MapFrom(c => c.ReturnArea.Region.RegionName))
                .ForMember(d => d.VendorCertificateFileNames, opt => opt.MapFrom(c => c.VendorCertificate.Select(c => c.CertificateImageFileName).ToArray()))

                .ReverseMap();
            CreateMap<Vendor, VendorInfoUpdateModel>().ReverseMap();


            CreateMap<VendorStoreSlider, VendorStoreSliderViewModel>().ReverseMap();
            //Vendor store Mapping
            CreateMap<Vendor, StoreViewModel>()
                .ForMember(d => d.ProductImageFileNames,
                    opt => opt.MapFrom(c => c.Product.Where(p => p.Published).Select(p => p.ProductBlob.First().BlobFileName).ToArray()))
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
            CreateMap<VendorStoreSlider, VendorSliderUnapprovedModel>()
                .ForMember(d => d.AuthorizedPerson, opt => opt.MapFrom(c => c.Vendor.AuthorizedPerson))
                .ForMember(d => d.Email, opt => opt.MapFrom(c => c.Vendor.Email))
                .ForMember(d => d.StoreName, opt => opt.MapFrom(c => c.Vendor.StoreName))
                .ForMember(d => d.VerifiedPhone, opt => opt.MapFrom(c => c.Vendor.StoreName))
                .ReverseMap();

            //Vendor ProductCategory Mapping
            CreateMap<VendorProductCategory, VendorProductCategoryAddModel>().ReverseMap();
            CreateMap<VendorProductCategory, VendorProductCategoryModel>().ReverseMap();
            CreateMap<VendorProductCategory, VendorProductCategoryDisplayModel>().ReverseMap();
            CreateMap<VendorProductCategory, VendorProductCategoryUpdateModel>().ReverseMap();
            CreateMap<VendorProductCategory, VendorProductCategoryUnapprovedModel>()
                .ForMember(d => d.AuthorizedPerson, opt => opt.MapFrom(c => c.Vendor.AuthorizedPerson))
                .ForMember(d => d.Email, opt => opt.MapFrom(c => c.Vendor.Email))
                .ForMember(d => d.StoreName, opt => opt.MapFrom(c => c.Vendor.StoreName))
                .ForMember(d => d.VerifiedPhone, opt => opt.MapFrom(c => c.Vendor.StoreName))
                .ReverseMap();


            //Vendor ProductCategory Assign Mapping
            CreateMap<VendorProductCategoryList, VendorProductCategoryAssignModel>().ReverseMap();


            CreateMap<VendorDataChangeApprovedModel, Vendor>().ReverseMap();


        }
    }
}