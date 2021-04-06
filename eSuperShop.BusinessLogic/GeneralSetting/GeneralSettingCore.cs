using AutoMapper;
using eSuperShop.Repository;
using System;

namespace eSuperShop.BusinessLogic
{
    public class GeneralSettingCore : IGeneralSettingCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public GeneralSettingCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public DbResponse ChangeOrderQuantityLimit(int quantity)
        {
            try
            {
                return _db.GeneralSetting.ChangeOrderQuantityLimit(quantity);

            }
            catch (Exception e)
            {
                return new DbResponse(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }

        public DbResponse<int> GetOrderQuantityLimit()
        {
            try
            {
                return _db.GeneralSetting.GetOrderQuantityLimit();

            }
            catch (Exception e)
            {
                return new DbResponse<int>(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            }
        }
    }
}