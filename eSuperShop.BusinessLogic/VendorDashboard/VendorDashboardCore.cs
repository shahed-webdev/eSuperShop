using AutoMapper;
using eSuperShop.Repository;
using System;

namespace eSuperShop.BusinessLogic
{
    public class VendorDashboardCore : IVendorDashboardCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public VendorDashboardCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public DbResponse<VendorDashboardModel> GetDetails(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return new DbResponse<VendorDashboardModel>(false, "Invalid Data");

                var vendorId = _db.Registration.VendorIdByUserName(userName);

                if (_db.Vendor.IsNull(vendorId))
                    return new DbResponse<VendorDashboardModel>(false, "No Data Found", null, "VerifiedPhone");


                var data = _db.Vendor.Dashboard(vendorId);

                return new DbResponse<VendorDashboardModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<VendorDashboardModel>(false, e.Message);
            }
        }


    }
}