using AutoMapper;
using eSuperShop.Repository;
using Paging.Infrastructure;
using System;

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

        public DbResponse<PagedResult<StoreViewModel>> TopStores(StoreFilterRequest model)
        {
            try
            {
                var data = _db.Vendor.TopStores(model);
                if (data.Results == null)
                    return new DbResponse<PagedResult<StoreViewModel>>(false, "No Data found");

                return new DbResponse<PagedResult<StoreViewModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<PagedResult<StoreViewModel>>(false, e.Message);
            }
        }
    }
}