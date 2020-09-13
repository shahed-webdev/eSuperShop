using AutoMapper;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public class SpecificationCore : ISpecificationCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public SpecificationCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }
        public DbResponse<SpecificationModel> Add(SpecificationAddModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse<SpecificationModel>(false, "Invalid User");

                model.CreatedByRegistrationId = registrationId;

                if (string.IsNullOrEmpty(model.KeyName))
                    return new DbResponse<SpecificationModel>(false, "Invalid Data");

                if (_db.Specification.IsExistName(model.KeyName))
                    return new DbResponse<SpecificationModel>(false, "Specification Name already Exist", null, "Name");

                _db.Specification.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<SpecificationModel>(_db.Specification.Specification);

                return new DbResponse<SpecificationModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<SpecificationModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                if (_db.Specification.IsNull(id))
                    return new DbResponse(false, "Data not found");
                if (_db.Specification.IsRelatedDataExist(id))
                    return new DbResponse(false, "Related data Exist");

                _db.Specification.Delete(id);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse AssignCatalog(SpecificationAssignModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse(false, "Invalid User");

                model.AssignedByRegistrationId = registrationId;

                _db.Specification.AssignCatalog(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DataResult<SpecificationModel> List(DataRequest request)
        {
            return _db.Specification.List(request);
        }

        public DbResponse<List<DDL>> Ddl()
        {
            try
            {
                var data = _db.Specification.ListDdl();
                return new DbResponse<List<DDL>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<DDL>>(false, e.Message);
            }
        }
    }
}