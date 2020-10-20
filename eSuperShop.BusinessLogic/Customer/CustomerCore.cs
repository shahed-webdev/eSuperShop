using AutoMapper;
using eSuperShop.Repository;
using System;

namespace eSuperShop.BusinessLogic
{
    public class CustomerCore : ICustomerCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public CustomerCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public DbResponse Add(CustomerAddModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.UserName))
                    return new DbResponse(false, "Invalid Data");

                _db.Customer.Add(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }
    }
}