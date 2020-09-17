using AutoMapper;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using OtpNet;
using Service.SMS;
using System;

namespace eSuperShop.BusinessLogic
{
    public class VendorCore : IVendorCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public VendorCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;

        }
        public DbResponse SendCode(string mobileNumber, int codeValidSecond)
        {
            try
            {
                if (_db.Vendor.IsExistPhone(mobileNumber)) return new DbResponse(false, "Phone number already exist");

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

        public DbResponse VerifyMobileNumber(string code, string mobileNumber)
        {
            try
            {
                long timeStepMatched;
                var verify = OtpServiceSingleton.Instance.Totp.VerifyTotp(code, out timeStepMatched, window: null);
                if (mobileNumber != OtpServiceSingleton.Instance.PhoneNunber) return new DbResponse(false, "Mobile number not match");
                if (!verify) return new DbResponse(false, "Invalid Code");

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }
        public DbResponse<VendorModel> Add(VendorAddModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.VerifiedPhone))
                    return new DbResponse<VendorModel>(false, "Invalid Data");

                if (_db.Vendor.IsExistPhone(model.VerifiedPhone))
                    return new DbResponse<VendorModel>(false, "Mobile Number already Exist", null, "Name");

                if (_db.Vendor.IsExistEmail(model.Email))
                    return new DbResponse<VendorModel>(false, "Email already Exist", null, "Name");

                _db.Vendor.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<VendorModel>(_db.Brand.Brand);

                return new DbResponse<VendorModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<VendorModel>(false, e.Message);
            }
        }
        public DataResult<VendorModel> List(DataRequest request)
        {
            throw new NotImplementedException();
        }

        public DbResponse Approved(int vendorId)
        {
            throw new NotImplementedException();
        }

        public DbResponse Unapproved(int vendorId)
        {
            throw new NotImplementedException();
        }

        public DbResponse<VendorDetailsModel> GetDetails(int vendorId)
        {
            throw new NotImplementedException();
        }

        public DbResponse AssignCatalogMultiple(VendorCatalogAssignModel model, string userName)
        {
            throw new NotImplementedException();
        }

        public DbResponse UnAssignCatalog(int vendorId, int catalogId)
        {
            throw new NotImplementedException();
        }

    }
}