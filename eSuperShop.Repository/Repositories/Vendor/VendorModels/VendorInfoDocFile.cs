using Microsoft.AspNetCore.Http;

namespace eSuperShop.Repository
{
    public class VendorInfoDocFile
    {
        public IFormFile FileStoreLogo { get; set; }
        public IFormFile FileNidPhotoFront { get; set; }
        public IFormFile FileNidPhotoBack { get; set; }
        public IFormFile FileTradeLicense { get; set; }
        public IFormFile[] FileOthersCertificate { get; set; }
        public IFormFile FileChequeCopy { get; set; }
        public IFormFile FileStoreBanner { get; set; }
    }
}