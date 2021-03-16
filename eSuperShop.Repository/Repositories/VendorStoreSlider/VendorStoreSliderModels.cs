namespace eSuperShop.Repository
{
    public class VendorSliderModel
    {
        public int VendorStoreSliderId { get; set; }
        public int VendorId { get; set; }
        public string ImageFileName { get; set; }
        public string RedirectUrl { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsApprovedByAdmin { get; set; }
    }

    public class VendorSliderUnapprovedModel
    {
        public int VendorStoreSliderId { get; set; }
        public int VendorId { get; set; }
        public string ImageFileName { get; set; }
        public string RedirectUrl { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
    }
    public class VendorSliderSlideModel
    {
        public int VendorStoreSliderId { get; set; }
        public string ImageFileName { get; set; }
        public string RedirectUrl { get; set; }
        public bool IsApprovedByAdmin { get; set; }
    }



}