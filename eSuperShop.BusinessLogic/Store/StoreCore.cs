using AutoMapper;
using eSuperShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.BusinessLogic
{
    public class StoreCore : IStoreCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public StoreCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public DbResponse<List<StoreViewModel>> TopStores(StoreFilterRequest model)
        {
            try
            {
                var data = _db.Vendor.TopStores(model);
                return new DbResponse<List<StoreViewModel>>(true, "Success", data.ToList());
            }
            catch (Exception e)
            {
                return new DbResponse<List<StoreViewModel>>(false, e.Message);
            }
        }
    }
}