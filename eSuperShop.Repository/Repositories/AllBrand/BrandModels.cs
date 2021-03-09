namespace eSuperShop.Repository
{
    public class BrandAddModel
    {
        public string Name { get; set; }
        public string LogoFileName { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }

    public class BrandEditModel
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string LogoFileName { get; set; }
    }

    public class BrandModel
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string LogoFileName { get; set; }
        public string CreatedBy { get; set; }
    }

    public class BrandAssignModel
    {
        public int BrandId { get; set; }
        public int CatalogId { get; set; }
        public int AssignedByRegistrationId { get; set; }
    }

    public class BrandAssignMultipleModel
    {
        public int AssignedByRegistrationId { get; set; }
        public int[] BrandIds { get; set; }
        public int[] CatalogIds { get; set; }
    }
}