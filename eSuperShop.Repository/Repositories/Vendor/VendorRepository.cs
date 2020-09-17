using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
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

        public bool IsExistPhone(string phone)
        {
            return Db.Vendor.Any(c => c.VerifiedPhone == phone);
        }

        public bool IsExistEmail(string email)
        {
            return Db.Vendor.Any(c => c.Email == email);
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
    }
}