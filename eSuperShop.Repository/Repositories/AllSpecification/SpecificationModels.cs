namespace eSuperShop.Repository
{

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
    public class SpecificationAssignModel
    {
        public int SpecificationId { get; set; }
        public int CatalogId { get; set; }
        public int AssignedByRegistrationId { get; set; }
    }
    public class SpecificationAssignMultipleModel
    {
        public int[] SpecificationIds { get; set; }
        public int[] CatalogIds { get; set; }
        public int AssignedByRegistrationId { get; set; }
    }

    public class SpecificationFilterModel
    {
        public int SpecificationId { get; set; }
        public string KeyName { get; set; }
        public string[] Values { get; set; }
    }
}