using AutoMapper;
using eSuperShop.Repository;
using eSuperShop.Repository.Repositories;
using JqueryDataTables.LoopsIT;
using System;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public class RegionCore : IRegionCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public RegionCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public DbResponse<RegionAddEditModel> Add(RegionAddEditModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.RegionName))
                    return new DbResponse<RegionAddEditModel>(false, "Invalid Data");

                if (_db.Region.IsExistName(model.RegionName))
                    return new DbResponse<RegionAddEditModel>(false, $" {model.RegionName} already Exist");

                return _db.Region.Add(model);

            }
            catch (Exception e)
            {
                return new DbResponse<RegionAddEditModel>(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DbResponse Edit(RegionAddEditModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.RegionName))
                    return new DbResponse(false, "Invalid Data");

                if (_db.Region.IsNull(model.RegionId))
                    return new DbResponse(false, "No data Found");

                if (_db.Region.IsExistName(model.RegionName, model.RegionId))
                    return new DbResponse(false, $" {model.RegionName} already Exist");


                return _db.Region.Edit(model);

            }
            catch (Exception e)
            {
                return new DbResponse(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                if (_db.Region.IsNull(id))
                    return new DbResponse(false, "No data Found");

                if (_db.Region.IsRelatedDataExist(id))
                    return new DbResponse(false, "Failed, Area Exist in this region");

                return _db.Region.Delete(id);

            }
            catch (Exception e)
            {
                return new DbResponse(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DbResponse<RegionAddEditModel> Get(int id)
        {
            try
            {
                if (_db.Region.IsNull(id))
                    return new DbResponse<RegionAddEditModel>(false, "No data Found");

                return _db.Region.Get(id);

            }
            catch (Exception e)
            {
                return new DbResponse<RegionAddEditModel>(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DataResult<RegionAddEditModel> List(DataRequest request)
        {
            return _db.Region.List(request);
        }

        public List<DDL> ListDdl()
        {
            return _db.Region.ListDdl();
        }
    }
}