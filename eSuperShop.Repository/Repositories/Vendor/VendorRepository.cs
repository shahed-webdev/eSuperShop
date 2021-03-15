using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using Paging.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSuperShop.Repository
{
    public class VendorRepository : Repository, IVendorRepository
    {
        public VendorRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public Vendor Vendor { get; set; }
        public VendorCatalog VendorCatalog { get; set; }
        public void Add(VendorAddModel model)
        {
            Vendor = _mapper.Map<Vendor>(model);
            Db.Vendor.Add(Vendor);
        }


        public VendorModel Get(int id)
        {
            return Db.Vendor
                 .ProjectTo<VendorModel>(_mapper.ConfigurationProvider)
                 .FirstOrDefault(c => c.VendorId == id);
        }

        public VendorDashboardModel Dashboard(int id)
        {
            var dashboard = new VendorDashboardModel
            {
                VendorInfo = Db.Vendor
                    .ProjectTo<VendorInfoModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefault(c => c.VendorId == id)
            };

            dashboard.Catalogs = Catalogs(id);

            return dashboard;
        }

        public bool IsExistPhone(string phone)
        {
            return Db.Vendor.Any(c => c.VerifiedPhone == phone);
        }

        public bool IsExistEmail(string email)
        {
            return Db.Vendor.Any(c => c.Email == email);
        }

        public bool IsExistStore(string store)
        {
            return Db.Vendor.Any(c => c.StoreName == store);
        }

        public bool IsExistStore(string store, int updateVendorId)
        {
            return Db.Vendor.Any(c => c.StoreName == store && c.VendorId != updateVendorId);
        }

        public bool IsExistSlugUrl(string slugUrl)
        {
            return Db.Vendor.Any(c => c.StoreSlugUrl == slugUrl);
        }
        public bool IsExistSlugUrl(string slugUrl, int updateVendorId)
        {
            return Db.Vendor.Any(c => c.StoreSlugUrl == slugUrl && c.VendorId != updateVendorId);
        }
        public bool IsNull(int id)
        {
            return !Db.Vendor.Any(c => c.VendorId == id);
        }

        public DataResult<VendorModel> List(DataRequest request)
        {
            var list = Db.Vendor
                .ProjectTo<VendorModel>(_mapper.ConfigurationProvider);

            return list.ToDataResult(request);
        }


        public async Task<ICollection<VendorModel>> SearchAsync(string key)
        {
            return await Db.Vendor
                .Where(c => c.VerifiedPhone.Contains(key) || c.StoreName.Contains(key))
                .ProjectTo<VendorModel>(_mapper.ConfigurationProvider)
                .Take(5)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public void AssignCatalogMultiple(VendorCatalogAssignModel model)
        {
            var assignList = new List<VendorCatalog>();


            foreach (var item in model.Catalogs)
            {
                if (!IsExistVendorInCatalog(model.VendorId, item.CatalogId))
                {
                    assignList.Add(new VendorCatalog
                    {
                        VendorId = model.VendorId,
                        CatalogId = item.CatalogId,
                        CommissionPercentage = item.CommissionPercentage,
                        AssignedByRegistrationId = model.AssignedByRegistrationId
                    });
                }
            }


            if (assignList.Any())
            {
                Db.VendorCatalog.AddRange(assignList);
            }
        }

        public void UnAssignCatalog(int VendorId, int catalogId)
        {
            VendorCatalog = Db.VendorCatalog.FirstOrDefault(c => c.VendorId == VendorId && c.CatalogId == catalogId);
            Db.VendorCatalog.Remove(VendorCatalog);
        }

        public bool IsExistVendorInCatalog(int vendorId, int catalogId)
        {
            return Db.VendorCatalog.Any(c => c.VendorId == vendorId && c.CatalogId == catalogId);
        }

        public List<VendorModel> CatalogWiseList(int catalogId)
        {
            var list = Db.VendorCatalog
                .Include(c => c.Vendor)
                .Where(c => c.CatalogId == catalogId)
                .Select(c => c.Vendor)
                .ProjectTo<VendorModel>(_mapper.ConfigurationProvider);
            return list.ToList();
        }

        public void Approved(VendorApprovedModel model)
        {
            var vendor = Db.Vendor.Find(model.VendorId);
            var registration = new Registration
            {
                UserName = vendor.Email,
                Validation = true,
                Type = UserType.Seller,
                Name = vendor.AuthorizedPerson,
                Phone = vendor.VerifiedPhone,
                Email = vendor.Email
            };
            vendor.Registration = registration;
            vendor.ApprovedByRegistrationId = model.ApprovedByRegistrationId;
            vendor.ApprovedOnUtc = DateTime.UtcNow;
            vendor.IsApproved = true;

            Db.Vendor.Update(vendor);
        }

        public void Delete(int vendorId)
        {
            var vendor = Db.Vendor
                .Include(v => v.VendorCatalog)
                .FirstOrDefault(v => v.VendorId == vendorId);
            Db.Vendor.Remove(vendor);
        }

        public bool IsApproved(int vendorId)
        {
            return Db.Vendor.Find(vendorId).IsApproved;
        }

        public string GetPhone(int vendorId)
        {
            return Db.Vendor.Find(vendorId).VerifiedPhone;
        }

        public List<VendorCatalogViewModel> Catalogs(int vendorId)
        {
            var list = Db
                .VendorCatalog
                .Include(v => v.Catalog)
                .ThenInclude(c => c.ParentCatalog)
                .AsEnumerable()?.ToList()
                .Select(c => new VendorCatalogViewModel
                {
                    CatalogId = c.CatalogId,
                    CatalogName = CatalogDllFunction(c.Catalog.ParentCatalog, c.Catalog.CatalogName),
                    CommissionPercentage = c.CommissionPercentage
                }).OrderBy(l => l.CatalogName);
            return list?.ToList() ?? new List<VendorCatalogViewModel>();



        }

        public bool IsCatalogExist(int vendorId, int catalogId)
        {
            return Db.VendorCatalog.Any(v => v.CatalogId == catalogId && v.VendorId == vendorId);
        }

        public List<DDL> ThemeDdl()
        {
            var list = from StoreTheme a in Enum.GetValues(typeof(StoreTheme))
                       select
                           new DDL
                           {
                               label = a.GetDescription(),
                               value = a.ToString()
                           };
            return list.ToList();
        }

        public void ThemeChange(int vendorId, StoreTheme theme)
        {
            var vendor = Db.Vendor.Find(vendorId);
            vendor.StoreTheme = theme;
            Db.Vendor.Update(vendor);
        }

        public void SlugUrlChange(int vendorId, string slugUrl)
        {
            var vendor = Db.Vendor.Find(vendorId);
            vendor.StoreSlugUrl = slugUrl;
            Db.Vendor.Update(vendor);
        }

        public void BanarUrlChange(int vendorId, string banarUrl)
        {
            var vendor = Db.Vendor.Find(vendorId);
            vendor.StoreBannerFileName = banarUrl;
            Db.Vendor.Update(vendor);
        }

        public void StoreInfoUpdate(VendorInfoUpdateModel model)
        {
            var vendor = Db.Vendor
                .Include(v => v.VendorCertificate)
                .FirstOrDefault(v => v.VendorId == model.VendorId);

            vendor.StoreBannerFileName = model.StoreBannerFileName;
            vendor.StoreLogoFileName = model.StoreLogoFileName;
            vendor.StoreTagLine = model.StoreTagLine;

            if (string.IsNullOrEmpty(model.StoreLogoFileName))
            {
                if (!String.Equals(vendor.StoreLogoFileName, model.StoreLogoFileName, StringComparison.CurrentCultureIgnoreCase))
                {
                    vendor.ChangedStoreLogoFileName = model.StoreLogoFileName;
                    vendor.IsChangedApproved = false;
                }
            }

            if (string.IsNullOrEmpty(model.StoreBannerFileName))
            {
                if (!String.Equals(vendor.StoreBannerFileName, model.StoreBannerFileName, StringComparison.CurrentCultureIgnoreCase))
                {
                    vendor.ChangedStoreBannerFileName = model.ChequeImageFileName;
                    vendor.IsChangedApproved = false;
                }
            }

            if (string.IsNullOrEmpty(model.StoreTagLine))
            {
                if (!String.Equals(vendor.StoreTagLine, model.StoreTagLine, StringComparison.CurrentCultureIgnoreCase))
                {
                    vendor.ChangedStoreTagLine = model.StoreTagLine;
                    vendor.IsChangedApproved = false;
                }
            }


            vendor.NId = model.NId;
            vendor.NIdImageFrontFileName = model.NIdImageFrontFileName;
            vendor.NIdImageBackFileName = model.NIdImageBackFileName;
            vendor.TradeLicenseImageFileName = model.TradeLicenseImageFileName;
            vendor.BankAccountTitle = model.BankAccountTitle;
            vendor.BankAccountNumber = model.BankAccountNumber;
            vendor.BankName = model.BankName;
            vendor.BranchName = model.BranchName;
            vendor.RoutingNumber = model.RoutingNumber;
            vendor.ChequeImageFileName = model.ChequeImageFileName;
            vendor.MobileBankingType = model.MobileBankingType;
            vendor.MobileBankingNumber = model.MobileBankingNumber;
            vendor.WarehouseAddress = model.WarehouseAddress;
            vendor.WarehousePhone = model.WarehousePhone;
            vendor.WarehousePostcode = model.WarehousePostcode;
            vendor.WarehouseAreaId = model.WarehouseAreaId;
            vendor.ReturnAddress = model.ReturnAddress;
            vendor.ReturnPhone = model.ReturnPhone;
            vendor.ReturnPostcode = model.ReturnPostcode;
            vendor.ReturnAreaId = model.ReturnAreaId;
            if (model.VendorCertificateFileNames != null)
            {
                vendor.VendorCertificate = model.VendorCertificateFileNames.Select(c => new VendorCertificate
                {
                    CertificateImageFileName = c
                }).ToList();
            }

            Db.Vendor.Update(vendor);
        }

        public VendorInfoModel ProfileDetails(int vendorId)
        {
            return Db.Vendor
                .Where(c => c.VendorId == vendorId)
                .ProjectTo<VendorInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public PagedResult<StoreViewModel> TopStores(StoreFilterRequest request)
        {
            var stores = Db.Vendor
                .Where(v => v.IsApproved)
                .ProjectTo<StoreViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.Rating).ThenBy(s => s.RatingBy)
                .GetPaged(request.Page, request.PageSize);
            return stores;
        }

        public StoreThemeViewModel StoreThemeDetails(string storeSlugUrl)
        {
            return Db.Vendor
                .Where(c => c.StoreSlugUrl == storeSlugUrl)
                .ProjectTo<StoreThemeViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public PagedResult<StoreProductViewModel> StoreProductList(StoreProductFilterRequest request)
        {
            return Db.VendorProductCategory
                .Include(v => v.VendorProductCategoryList)
                .ThenInclude(l => l.Product)
                .Where(c => c.VendorId == request.VendorId && c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ProjectTo<StoreProductViewModel>(_mapper.ConfigurationProvider)
                .GetPaged(request.Page, request.PageSize);
        }

        public DataResult<VendorDataChangeApprovedModel> DataChangeUnapprovedList(DataRequest request)
        {
            return Db.Vendor
                .Where(c => !c.IsChangedApproved)
                .ProjectTo<VendorDataChangeApprovedModel>(_mapper.ConfigurationProvider)
                .ToDataResult(request);
        }

        public List<string> DataChangeApproved(VendorDataChangeApprovedModel model)
        {
            var urlList = new List<string>();
            var vendor = Db.Vendor.Find(model.VendorId);

            if (string.IsNullOrEmpty(model.ChangedStoreLogoFileName))
            {
                if (!string.Equals(vendor.StoreLogoFileName, model.StoreLogoFileName, StringComparison.CurrentCultureIgnoreCase))
                {
                    urlList.Add(vendor.StoreLogoFileName);
                    vendor.StoreLogoFileName = model.ChangedStoreLogoFileName;
                    vendor.ChangedStoreLogoFileName = string.Empty;

                }
            }

            if (string.IsNullOrEmpty(model.ChangedStoreBannerFileName))
            {
                if (!string.Equals(vendor.StoreBannerFileName, model.StoreLogoFileName, StringComparison.CurrentCultureIgnoreCase))
                {
                    urlList.Add(vendor.StoreBannerFileName);

                    vendor.StoreBannerFileName = model.ChangedStoreBannerFileName;
                    vendor.ChangedStoreBannerFileName = string.Empty;
                }
            }

            if (string.IsNullOrEmpty(model.ChangedStoreTagLine))
            {
                if (!string.Equals(vendor.StoreTagLine, model.ChangedStoreTagLine, StringComparison.CurrentCultureIgnoreCase))
                {
                    vendor.ChangedStoreLogoFileName = model.StoreLogoFileName;
                    vendor.ChangedStoreTagLine = string.Empty;
                }
            }

            vendor.IsChangedApproved = true;

            Db.Vendor.Update(vendor);
            return urlList;
        }

        public void VendorInfoUpdateByAdmin(VendorInfoUpdateByAdminModel model)
        {
            throw new NotImplementedException();
        }

        string CatalogDllFunction(Catalog catalog, string cat)
        {

            if (catalog != null)
            {
                cat = CatalogDllFunction(catalog.ParentCatalog, catalog.CatalogName) + ">" + cat;
            }

            return cat;
        }
    }
}