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