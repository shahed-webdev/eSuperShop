﻿using AutoMapper;
using eSuperShop.Repository;
using System;
using System.Collections.Generic;

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

        public DbResponse AddressAdd(CustomerAddressBookModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Address))
                    return new DbResponse(false, "Invalid Data");

                if (_db.Customer.IsThreeAddressSaved(model.CustomerId))
                    return new DbResponse(false, "Already three address added");

                _db.Customer.AddressAdd(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }


        public DbResponse<List<CustomerAddressBookModel>> AddressList(int customerId)
        {
            try
            {
                var data = _db.Customer.AddressList(customerId);
                return new DbResponse<List<CustomerAddressBookModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<CustomerAddressBookModel>>(false, e.Message);
            }
        }
    }
}