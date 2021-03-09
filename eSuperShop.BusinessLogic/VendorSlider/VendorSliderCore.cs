using AutoMapper;
using eSuperShop.Repository;
using System;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public class VendorSliderCore : IVendorSliderCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public VendorSliderCore(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public DbResponse<VendorSliderModel> Add(VendorSliderModel model, string vendorUserName)
        {
            try
            {
                if (string.IsNullOrEmpty(model.ImageFileName)) return new DbResponse<VendorSliderModel>(false, "Invalid Data");

                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0)
                    return new DbResponse<VendorSliderModel>(false, "Invalid User");

                model.VendorId = vendorId;
                _db.VendorStoreSlider.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<VendorSliderModel>(_db.VendorStoreSlider.VendorStoreSlider);

                return new DbResponse<VendorSliderModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<VendorSliderModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int vendorStoreSliderId)
        {
            try
            {
                if (!_db.VendorStoreSlider.IsNull(vendorStoreSliderId)) return new DbResponse(false, "No data Found");

                _db.VendorStoreSlider.Delete(vendorStoreSliderId);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<List<VendorSliderSlideModel>> Display(string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0)
                    return new DbResponse<List<VendorSliderSlideModel>>(false, "Invalid User");
                var data = _db.VendorStoreSlider.Display(vendorId);



                return new DbResponse<List<VendorSliderSlideModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<VendorSliderSlideModel>>(false, e.Message);
            }
        }

        public DbResponse<List<VendorSliderModel>> List(string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0)
                    return new DbResponse<List<VendorSliderModel>>(false, "Invalid User");

                var data = _db.VendorStoreSlider.List(vendorId);
                return new DbResponse<List<VendorSliderModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<VendorSliderModel>>(false, e.Message);
            }
        }
    }
}