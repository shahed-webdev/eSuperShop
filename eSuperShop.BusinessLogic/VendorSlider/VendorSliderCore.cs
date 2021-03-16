using AutoMapper;
using CloudStorage;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<DbResponse<VendorSliderModel>> AddAsync(VendorSliderModel model, string vendorUserName, ICloudStorage cloudStorage, IFormFile file)
        {
            try
            {
                if (file == null) return new DbResponse<VendorSliderModel>(false, "No image file found");

                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0)
                    return new DbResponse<VendorSliderModel>(false, "Invalid User");

                var fileName = FileBuilder.FileNameImage("store", file.FileName);
                model.ImageFileName = await cloudStorage.UploadFileAsync(file, fileName);

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

        public DataResult<VendorSliderUnapprovedModel> SliderUnapprovedList(DataRequest request)
        {
            var data = _db.VendorStoreSlider.SliderUnapprovedList(request);
            return data;
        }
    }
}