using AutoMapper;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public class WarehouseCore : IWarehouseCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public WarehouseCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }
        public DbResponse<WarehouseModel> Add(WarehouseAddModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse<WarehouseModel>(false, "Invalid User");

                model.CreatedByRegistrationId = registrationId;

                if (string.IsNullOrEmpty(model.Name))
                    return new DbResponse<WarehouseModel>(false, "Invalid Data");

                if (_db.Warehouse.IsExistName(model.Name))
                    return new DbResponse<WarehouseModel>(false, "Warehouse Name already Exist", null, "Name");

                _db.Warehouse.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<WarehouseModel>(_db.Warehouse.Warehouse);

                return new DbResponse<WarehouseModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<WarehouseModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                if (_db.Warehouse.IsNull(id))
                    return new DbResponse(false, "Data not found");
                if (_db.Warehouse.IsRelatedDataExist(id))
                    return new DbResponse(false, "Related data Exist");

                _db.Warehouse.Delete(id);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse AssignCatalog(WarehouseAssignModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse(false, "Invalid User");

                model.AssignedByRegistrationId = registrationId;

                _db.Warehouse.AssignCatalog(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DataResult<WarehouseModel> List(DataRequest request)
        {
            return _db.Warehouse.List(request);
        }

        public DbResponse<List<DDL>> Ddl()
        {
            try
            {
                var data = _db.Warehouse.ListDdl();
                return new DbResponse<List<DDL>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<DDL>>(false, e.Message);
            }
        }
    }
}