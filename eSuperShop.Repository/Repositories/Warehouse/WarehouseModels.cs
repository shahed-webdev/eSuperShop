namespace eSuperShop.Repository
{
    public class WarehouseAssignModel
    {
        public int VendorId { get; set; }
        public int WarehouseId { get; set; }
        public int AssignedByRegistrationId { get; set; }

    }

    public class WarehouseAddModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }

    public class WarehouseModel
    {
        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public string CreatedBy { get; set; }
    }
}