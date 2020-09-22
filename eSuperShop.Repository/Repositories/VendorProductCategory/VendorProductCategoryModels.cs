namespace eSuperShop.Repository
{
    public class VendorProductCategoryAddModel
    {
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? DisplayOrder { get; set; }
    }

    public class VendorProductCategoryModel
    {
        public int VendorProductCategoryId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }

    public class VendorProductCategoryUpdateModel
    {
        public int VendorProductCategoryId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }

    public class VendorProductCategoryDisplayModel
    {
        public int VendorProductCategoryId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
    public class VendorProductCategoryAssignModel
    {
        public int VendorProductCategoryId { get; set; }
        public int ProductId { get; set; }
    }
    public class VendorCategoryProductsModel
    {
    }
}