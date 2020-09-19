using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class VendorStoreSliderRepository : Repository, IVendorStoreSliderRepository
    {
        public VendorStoreSliderRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public VendorStoreSlider VendorStoreSlider { get; set; }
        public void Add(VendorSliderModel model)
        {
            VendorStoreSlider = _mapper.Map<VendorStoreSlider>(model);
            Db.VendorStoreSlider.Add(VendorStoreSlider);
        }

        public void Delete(int vendorStoreSliderId)
        {
            VendorStoreSlider = Db.VendorStoreSlider.Find(vendorStoreSliderId);
            Db.VendorStoreSlider.Remove(VendorStoreSlider);
        }

        public bool IsNull(int vendorStoreSliderId)
        {
            return Db.VendorStoreSlider.Any(s => s.VendorStoreSliderId == vendorStoreSliderId);
        }

        public List<VendorSliderSlideModel> Display(int vendorId)
        {
            return Db.VendorStoreSlider
                .Where(s => s.VendorId == vendorId)
                .ProjectTo<VendorSliderSlideModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public List<VendorSliderModel> List(int vendorId)
        {
            return Db.VendorStoreSlider
                .Where(s => s.VendorId == vendorId)
                .ProjectTo<VendorSliderModel>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}