namespace eSuperShop.Repository
{
    public class SpecificationAssignModel
    {
        public int SpecificationId { get; set; }
        public int CatalogId { get; set; }
        public int AssignedByRegistrationId { get; set; }
    }

    public class SpecificationAddModel
    {
        public string KeyName { get; set; }
        public bool AllowFiltering { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }

    public class SpecificationModel
    {
        public int SpecificationId { get; set; }
        public string KeyName { get; set; }
        public bool AllowFiltering { get; set; }
        public string CreatedBy { get; set; }
    }
}