namespace eSuperShop.Repository
{
    public class VendorAddModel
    {
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string StoreSlugUrl { get; set; }
        public string Email { get; set; }
        public string StoreAddress { get; set; }
        public string StorePostcode { get; set; }
        public int StoreAreaId { get; set; }
    }
}