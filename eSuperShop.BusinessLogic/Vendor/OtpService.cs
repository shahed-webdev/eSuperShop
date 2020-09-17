using OtpNet;
using System;

namespace eSuperShop.BusinessLogic
{

    public class OtpServiceSingleton
    {
        private static readonly Lazy<OtpServiceSingleton> lazy = new Lazy<OtpServiceSingleton>(() => new OtpServiceSingleton());
        private OtpServiceSingleton()
        {
            var bytes = Base32Encoding.ToBytes("JBSWY3DPEHPK3PXP");

            Totp = new Totp(bytes);
        }

        public Totp Totp { get; set; }
        public string PhoneNunber { get; set; }
        public static OtpServiceSingleton Instance => lazy.Value;
    }
}