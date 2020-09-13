﻿namespace eSuperShop.Repository
{
    public class AttributeAssignModel
    {
        public int CatalogId { get; set; }
        public int AttributeId { get; set; }
        public int AssignedByRegistrationId { get; set; }
    }

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
}