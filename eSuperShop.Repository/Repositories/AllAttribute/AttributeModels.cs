namespace eSuperShop.Repository
{

    public class AttributeAddModel
    {
        public string KeyName { get; set; }
        public bool AllowFiltering { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }

    public class AttributeModel
    {
        public int AttributeId { get; set; }
        public string KeyName { get; set; }
        public bool AllowFiltering { get; set; }
        public string CreatedBy { get; set; }
    }

    public class AttributeAssignModel
    {
        public int CatalogId { get; set; }
        public int AttributeId { get; set; }
        public int AssignedByRegistrationId { get; set; }
    }
    public class AttributeAssignMultipleModel
    {
        public int[] CatalogIds { get; set; }
        public int[] AttributeIds { get; set; }
        public int AssignedByRegistrationId { get; set; }
    }
}