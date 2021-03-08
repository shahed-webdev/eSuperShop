namespace eSuperShop.Data
{
    public class VendorProductCategoryList
    {
        public int VendorProductCategoryListId { get; set; }
        public int VendorProductCategoryId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual VendorProductCategory VendorProductCategory { get; set; }
        public bool IsApprovedByAdmin { get; set; }
    }
}