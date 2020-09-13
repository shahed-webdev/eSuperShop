namespace eSuperShop.Repository
{
    public class BrandAddModel
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }

    public class BrandModel
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string CreatedBy { get; set; }
    }

    public class BrandAssignModel
    {
        public int BrandId { get; set; }
        public int CatalogId { get; set; }
        public int AssignedByRegistrationId { get; set; }
    }
}