namespace eSuperShop.Repository
{

    public class SeoAddModel
    {
        public int AssignTableId { get; set; }
        public int SeoId { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }

    public class SeoModel
    {
        public int SeoId { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string CreatedBy { get; set; }
    }
}