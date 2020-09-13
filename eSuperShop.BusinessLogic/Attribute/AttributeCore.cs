﻿using AutoMapper;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public class AttributeCore : IAttributeCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public AttributeCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }
        public DbResponse<AttributeModel> Add(AttributeAddModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse<AttributeModel>(false, "Invalid User");

                model.CreatedByRegistrationId = registrationId;

                if (string.IsNullOrEmpty(model.KeyName))
                    return new DbResponse<AttributeModel>(false, "Invalid Data");

                if (_db.Attribute.IsExistName(model.KeyName))
                    return new DbResponse<AttributeModel>(false, "Attribute Name already Exist", null, "Name");

                _db.Attribute.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<AttributeModel>(_db.Attribute.Attribute);

                return new DbResponse<AttributeModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<AttributeModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                if (_db.Attribute.IsNull(id))
                    return new DbResponse(false, "Data not found");
                if (_db.Attribute.IsRelatedDataExist(id))
                    return new DbResponse(false, "Related data Exist");

                _db.Attribute.Delete(id);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse AssignCatalog(AttributeAssignModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse(false, "Invalid User");

                model.AssignedByRegistrationId = registrationId;

                _db.Attribute.AssignCatalog(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DataResult<AttributeModel> List(DataRequest request)
        {
            return _db.Attribute.List(request);
        }

        public DbResponse<List<DDL>> Ddl()
        {
            try
            {
                var data = _db.Attribute.ListDdl();
                return new DbResponse<List<DDL>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<DDL>>(false, e.Message);
            }
        }
    }
}