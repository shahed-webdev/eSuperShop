using AutoMapper;
using eSuperShop.Repository;
using eSuperShop.Repository.Repositories;
using JqueryDataTables.LoopsIT;
using System;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public class AreaCore : IAreaCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public AreaCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }
        public DbResponse<AreaAddEditModel> Add(AreaAddEditModel model)
        {
            try
            {
                return _db.Area.Add(model);

            }
            catch (Exception e)
            {
                return new DbResponse<AreaAddEditModel>(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DbResponse Edit(AreaAddEditModel model)
        {
            try
            {
                return _db.Area.Edit(model);

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
                return _db.Area.Delete(id);

            }
            catch (Exception e)
            {
                return new DbResponse(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DbResponse<AreaAddEditModel> Get(int id)
        {
            try
            {
                return _db.Area.Get(id);

            }
            catch (Exception e)
            {
                return new DbResponse<AreaAddEditModel>(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DataResult<AreaAddEditModel> List(DataRequest request)
        {
            return _db.Area.List(request);
        }

        public List<DDL> ListDdl()
        {

            return _db.Area.ListDdl();

        }

        public List<DDL> ListDdlRegionWise(int regionId)
        {
            return _db.Area.ListDdlRegionWise(regionId);
        }

        public List<AreaAddEditModel> GetRegionWiseArea(int regionId)
        {
            return _db.Area.GetRegionWiseArea(regionId);
        }
    }
}