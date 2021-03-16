namespace eSuperShop.Repository
{
    public class VendorProductCategoryAddModel
    {
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public int? DisplayOrder { get; set; }
    }

    public class VendorProductCategoryModel
    {
        public int VendorProductCategoryId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
    }

    public class VendorProductCategoryUpdateModel
    {
        public int VendorProductCategoryId { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
    }

    public class VendorProductCategoryDisplayModel
    {
        public int VendorProductCategoryId { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
    }

    public class VendorProductCategoryUnapprovedModel
    {
        public int VendorProductCategoryId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public string ChangedName { get; set; }
        public string ChangedImageFileName { get; set; }
        public string AuthorizedPerson { get; set; }
        public string VerifiedPhone { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
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