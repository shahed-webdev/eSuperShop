using AutoMapper;
using eSuperShop.Data;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Identity;
using OtpNet;
using Service.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public class VendorCore : IVendorCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;
        private readonly UserManager<IdentityUser> _userManager;
        public VendorCore(IMapper mapper, IUnitOfWork db, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _db = db;
            _userManager = userManager;
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
                    return new DbResponse<VendorModel>(false, "Mobile Number already Exist", null, "VerifiedPhone");
                if (model.VerifiedPhone != OtpServiceSingleton.Instance.PhoneNunber)
                    return new DbResponse<VendorModel>(false, "Mobile number not Verified", null, "VerifiedPhone");
                if (_db.Vendor.IsExistEmail(model.Email))
                    return new DbResponse<VendorModel>(false, "Email already Exist", null, "Email");
                if (_db.Vendor.IsExistStore(model.StoreName))
                    return new DbResponse<VendorModel>(false, "Store Name already Exist", null, "Email");

                _db.Vendor.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<VendorModel>(_db.Vendor.Vendor);

                return new DbResponse<VendorModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<VendorModel>(false, e.Message);
            }
        }
        public DataResult<VendorModel> List(DataRequest request)
        {
            return _db.Vendor.List(request);
        }

        public async Task<DbResponse> ApprovedAsync(VendorApprovedModel model, string userName)
        {
            try
            {

                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse(false, "Invalid User");

                model.ApprovedByRegistrationId = registrationId;

                if (_db.Vendor.IsNull(model.VendorId))
                    return new DbResponse(false, "Vendor not Found");

                var vendor = _db.Vendor.Get(model.VendorId);

                //Identity Create
                var user = new IdentityUser { UserName = vendor.Email, Email = vendor.Email };
                var password = Password.Generate(8, 3);
                var result = await _userManager.CreateAsync(user, password).ConfigureAwait(false);

                if (!result.Succeeded) return new DbResponse(false, result.Errors.FirstOrDefault()?.Description);

                await _userManager.AddToRoleAsync(user, UserType.Seller.ToString()).ConfigureAwait(false);


                //Update vendor table
                _db.Vendor.Approved(model);
                _db.SaveChanges();


                #region SMS Code

                var textSms = $"Your Account Successfully activated, Your Id: {vendor.Email} & password: {password} . Please change your password for better security";

                var massageLength = SmsValidator.MassageLength(textSms);
                var smsCount = SmsValidator.TotalSmsCount(textSms);

                var smsProvider = new SmsProviderBuilder();

                var smsBalance = smsProvider.SmsBalance();
                if (smsBalance < smsCount) return new DbResponse(false, "No SMS Balance");

                var providerSendId = smsProvider.SendSms(textSms, vendor.VerifiedPhone);

                if (!smsProvider.IsSuccess) return new DbResponse(false, smsProvider.Error);

                #endregion


                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse Delete(int vendorId)
        {
            try
            {
                if (_db.Vendor.IsNull(vendorId))
                    return new DbResponse(false, "Vendor not Found");
                if (_db.Vendor.IsApproved(vendorId))
                    return new DbResponse(false, "Approved Vendor cannot be deleted");

                _db.Vendor.Delete(vendorId);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<VendorModel> GetDetails(int vendorId)
        {
            try
            {

                if (_db.Vendor.IsNull(vendorId))
                    return new DbResponse<VendorModel>(false, "No Data Found", null, "VerifiedPhone");


                var data = _db.Vendor.Get(vendorId);

                return new DbResponse<VendorModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<VendorModel>(false, e.Message);
            }
        }

        public DbResponse AssignCatalogMultiple(VendorCatalogAssignModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse(false, "Invalid User");
                model.AssignedByRegistrationId = registrationId;

                _db.Vendor.AssignCatalogMultiple(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse UnAssignCatalog(int vendorId, int catalogId)
        {
            try
            {
                if (!_db.Brand.IsExistBrandInCatalog(vendorId, catalogId)) return new DbResponse(false, "Data Not Found");
                _db.Vendor.UnAssignCatalog(vendorId, catalogId);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }
        public List<DDL> ThemeDdl()
        {
            return _db.Vendor.ThemeDdl();
        }

        public DbResponse ThemeChange(int vendorId, StoreTheme theme)
        {
            try
            {
                if (_db.Vendor.IsNull(vendorId))
                    return new DbResponse(false, "No Data Found");


                _db.Vendor.ThemeChange(vendorId, theme);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse SlugUrlChange(int vendorId, string slugUrl)
        {
            try
            {
                if (_db.Vendor.IsNull(vendorId))
                    return new DbResponse(false, "No Data Found");
                if (_db.Vendor.IsExistSlugUrl(slugUrl))
                    return new DbResponse(false, "SlugUrl Already Exist");

                _db.Vendor.SlugUrlChange(vendorId, slugUrl);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse BannerUrlChange(int vendorId, string banarUrl)
        {
            try
            {
                if (_db.Vendor.IsNull(vendorId))
                    return new DbResponse(false, "No Data Found");

                _db.Vendor.BanarUrlChange(vendorId, banarUrl);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse StoreUpdate(VendorInfoUpdateModel model, string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0)
                    return new DbResponse(false, "Invalid User");

                model.VendorId = vendorId;

                if (_db.Vendor.IsNull(vendorId))
                    return new DbResponse(false, "No Data Found");

                _db.Vendor.StoreInfoUpdate(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<VendorInfoUpdateModel> StoreDetails(string vendorUserName)
        {
            try
            {
                var vendorId = _db.Registration.VendorIdByUserName(vendorUserName);
                if (vendorId == 0)
                    return new DbResponse<VendorInfoUpdateModel>(false, "Invalid User");



                if (_db.Vendor.IsNull(vendorId))
                    return new DbResponse<VendorInfoUpdateModel>(false, "No Data Found");

                var data = _db.Vendor.StoreDetails(vendorId);

                return new DbResponse<VendorInfoUpdateModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<VendorInfoUpdateModel>(false, e.Message);
            }
        }
    }
}