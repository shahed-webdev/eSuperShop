namespace eSuperShop.Data
{
    public class VendorCertificate
    {
        public int VendorCertificateId { get; set; }
        public int VendorId { get; set; }
        public string CertificateImageUrl { get; set; }
        public Vendor Vendor { get; set; }
    }
}