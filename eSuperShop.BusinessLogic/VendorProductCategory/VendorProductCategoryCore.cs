using AutoMapper;
using eSuperShop.Repository;
using System;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public class VendorProductCategoryCore : IVendorProductCategoryCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public VendorProductCategoryCore(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public DbResponse<VendorProductCategoryModel> Add(VendorProductCategoryAddModel model, string vendorUserName)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name)) return new DbResponse<VendorProductCategoryModel>(false, "Invalid Data");

                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0)
                    return new DbResponse<VendorProductCategoryModel>(false, "Invalid User");

                model.VendorId = vendorId;
                _db.VendorProductCategory.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<VendorProductCategoryModel>(_db.VendorProductCategory.VendorProductCategory);

                return new DbResponse<VendorProductCategoryModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<VendorProductCategoryModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                if (_db.VendorProductCategory.IsNull(id))
                    return new DbResponse(false, "No data Found");
                if (_db.VendorProductCategory.IsRelatedDataExist(id))
                    return new DbResponse(false, "Related data Found");

                _db.VendorProductCategory.Delete(id);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse Update(VendorProductCategoryUpdateModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name)) return new DbResponse(false, "Invalid Data");

                if (_db.VendorProductCategory.IsNull(model.VendorProductCategoryId))
                    return new DbResponse(false, "No Data Found");

                if (_db.VendorProductCategory.IsExistName(model.Name, model.VendorProductCategoryId))
                    return new DbResponse(false, "Store Name already Exist");

                _db.VendorProductCategory.Update(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<List<VendorProductCategoryDisplayModel>> DisplayList(string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0)
                    return new DbResponse<List<VendorProductCategoryDisplayModel>>(false, "Invalid User");


                var data = _db.VendorProductCategory.DisplayList(vendorId);

                return new DbResponse<List<VendorProductCategoryDisplayModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<VendorProductCategoryDisplayModel>>(false, e.Message);
            }
        }

        public DbResponse<List<DDL>> ListDdl(string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0)
                    return new DbResponse<List<DDL>>(false, "Invalid User");


                var data = _db.VendorProductCategory.ListDdl(vendorId);

                return new DbResponse<List<DDL>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<DDL>>(false, e.Message);
            }
        }

        public DbResponse<VendorProductCategoryUpdateModel> Get(int id)
        {
            try
            {
                var data = _db.VendorProductCategory.Get(id);

                return new DbResponse<VendorProductCategoryUpdateModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<VendorProductCategoryUpdateModel>(false, e.Message);
            }
        }

        public DbResponse PlaceAssign(VendorProductCategoryAssignModel model)
        {
            try
            {
                if (_db.VendorProductCategory.IsPlaceAssign(model))
                    return new DbResponse(false, "Already Assigned");

                _db.VendorProductCategory.PlaceAssign(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse PlaceDelete(VendorProductCategoryAssignModel model)
        {
            try
            {
                if (!_db.VendorProductCategory.IsPlaceAssign(model))
                    return new DbResponse(false, "Not Assigned");

                _db.VendorProductCategory.PlaceDelete(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }
    }
}