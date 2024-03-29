﻿using AutoMapper;
using CloudStorage;
using eSuperShop.Data;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public class SliderCore : ISliderCore
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;
        public SliderCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }
        public async Task<DbResponse<SliderListModel>> AddAsync(SliderAddModel model, string userName, ICloudStorage cloudStorage, IFormFile file)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse<SliderListModel>(false, "Invalid User");

                model.CreatedByRegistrationId = registrationId;

                if (file == null) return new DbResponse<SliderListModel>(false, "Invalid Data");

                var fileName = FileBuilder.FileNameImage("slider", file.FileName);
                model.ImageFileName = await cloudStorage.UploadFileAsync(file, fileName);


                _db.Slider.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<SliderListModel>(_db.Slider.Slider);

                return new DbResponse<SliderListModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<SliderListModel>(false, e.Message);
            }
        }
        public DbResponse Delete(int id)
        {
            try
            {
                if (!_db.Slider.IsNull(id)) return new DbResponse(false, "No data Found");

                _db.Slider.Delete(id);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<List<SliderSlideModel>> Display(SliderDisplayPlace place)
        {
            try
            {
                var data = _db.Slider.Display(place);
                return new DbResponse<List<SliderSlideModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<SliderSlideModel>>(false, e.Message);
            }
        }
        public DbResponse<List<SliderListModel>> List()
        {
            try
            {
                var data = _db.Slider.List();
                return new DbResponse<List<SliderListModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<SliderListModel>>(false, e.Message);
            }
        }
        public List<DDL> DisplayPlaceDdl()
        {
            return _db.Slider.SliderPlaceDdl();
        }
    }
}