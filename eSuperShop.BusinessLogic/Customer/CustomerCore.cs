using AutoMapper;
using eSuperShop.Data;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Identity;
using OtpNet;
using Service.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public class CustomerCore : ICustomerCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerCore(IMapper mapper, IUnitOfWork db, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _db = db;
            _userManager = userManager;
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

        public DbResponse AddressAdd(CustomerAddressBookModel model, string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Address))
                    return new DbResponse(false, "Invalid Data");

                if (_db.Customer.IsThreeAddressSaved(model.CustomerId))
                    return new DbResponse(false, "Already three address added");

                var customerId = _db.Registration.CustomerIdByUserName(userName);

                if (customerId == 0)
                    return new DbResponse(false, "Invalid User");

                model.CustomerId = customerId;
                _db.Customer.AddressAdd(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }


        public DbResponse<List<CustomerAddressBookModel>> AddressList(string userName)
        {
            try
            {
                var customerId = _db.Registration.CustomerIdByUserName(userName);

                if (customerId == 0)
                    return new DbResponse<List<CustomerAddressBookModel>>(false, "Invalid User");

                var data = _db.Customer.AddressList(customerId);

                return new DbResponse<List<CustomerAddressBookModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<CustomerAddressBookModel>>(false, e.Message);
            }
        }

        public DbResponse SendCode(string mobileNumber, int codeValidSecond)
        {
            try
            {
                if (_userManager.Users.Any(u => u.UserName == mobileNumber)) return new DbResponse(false, "Phone number already exist");

                OtpServiceSingleton.Instance.PhoneNunber = mobileNumber;
                #region Generate Code
                var bytes = Base32Encoding.ToBytes("JBSWY3DPEHPK3PXP");

                OtpServiceSingleton.Instance.Totp = new Totp(bytes, codeValidSecond);

                var code = OtpServiceSingleton.Instance.Totp.ComputeTotp(DateTime.UtcNow);

                var textSms = $"Your Verification code is {code}. This code is valid for {codeValidSecond} second";
                #endregion

                #region SMS Code

                var massageLength = SmsValidator.MassageLength(textSms);
                var smsCount = SmsValidator.TotalSmsCount(textSms);

                var smsProvider = new SmsProviderBuilder();

                var smsBalance = smsProvider.SmsBalance();
                if (smsBalance < smsCount) return new DbResponse(false, "No SMS Balance");

                var providerSendId = smsProvider.SendSms(textSms, mobileNumber);

                if (!smsProvider.IsSuccess) return new DbResponse(false, smsProvider.Error);

                #endregion

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public async Task<DbResponse> MobileSignUpAsync(CustomerMobileSignUpModel model)
        {
            try
            {
                long timeStepMatched;
                var verify = OtpServiceSingleton.Instance.Totp.VerifyTotp(model.Code, out timeStepMatched, window: null);
                if (model.MobileNumber != OtpServiceSingleton.Instance.PhoneNunber) return new DbResponse(false, "Mobile number not match");
                if (!verify) return new DbResponse(false, "Invalid Code");

                //Identity Create
                var user = new IdentityUser { UserName = model.MobileNumber };
                var password = model.Password;
                var result = await _userManager.CreateAsync(user, password).ConfigureAwait(false);

                if (!result.Succeeded) return new DbResponse(false, result.Errors.FirstOrDefault()?.Description);

                await _userManager.AddToRoleAsync(user, UserType.Customer.ToString()).ConfigureAwait(false);

                var customer = new CustomerAddModel
                {
                    UserName = model.MobileNumber
                };
                _db.Customer.Add(customer);
                _db.SaveChanges();


                #region SMS Code

                var textSms = $"You have registered Successfully , Your Id: {model.MobileNumber}";

                var massageLength = SmsValidator.MassageLength(textSms);
                var smsCount = SmsValidator.TotalSmsCount(textSms);

                var smsProvider = new SmsProviderBuilder();

                var smsBalance = smsProvider.SmsBalance();
                if (smsBalance < smsCount) return new DbResponse(false, "No SMS Balance");

                var providerSendId = smsProvider.SendSms(textSms, model.MobileNumber);

                if (!smsProvider.IsSuccess) return new DbResponse(false, smsProvider.Error);

                #endregion

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }
    }
}