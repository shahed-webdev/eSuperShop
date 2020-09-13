using AutoMapper;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public class BrandCore : IBrandCore
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public BrandCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }
        public DbResponse<BrandModel> Add(BrandAddModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse<BrandModel>(false, "Invalid User");

                model.CreatedByRegistrationId = registrationId;

                if (string.IsNullOrEmpty(model.Name))
                    return new DbResponse<BrandModel>(false, "Invalid Data");

                if (_db.Brand.IsExistName(model.Name))
                    return new DbResponse<BrandModel>(false, "Brand Name already Exist", null, "Name");

                _db.Brand.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<BrandModel>(_db.Brand.brand);

                return new DbResponse<BrandModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<BrandModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                if (_db.Brand.IsNull(id))
                    return new DbResponse(false, "Data not found");
                if (_db.Brand.IsRelatedDataExist(id))
                    return new DbResponse(false, "Related data Exist");

                _db.Brand.Delete(id);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse AssignCatalog(BrandAssignModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse(false, "Invalid User");

                model.AssignedByRegistrationId = registrationId;

                _db.Brand.AssignCatalog(model);
                _db.SaveChanges();

                var data = _db.Catalog.Get(model.CatalogId);

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DataResult<BrandModel> List(DataRequest request)
        {
            return _db.Brand.List(request);
        }

        public DbResponse<List<DDL>> Ddl()
        {
            try
            {
                var data = _db.Brand.ListDdl();
                return new DbResponse<List<DDL>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<DDL>>(false, e.Message);
            }
        }
    }
}