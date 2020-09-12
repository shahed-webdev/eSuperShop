namespace eSuperShop.Data
{
    public class ProductAttributeSetList
    {
        public int ProductAttributeSetListId { get; set; }
        public int ProductAttributeSetId { get; set; }
        public int ProductAttributeId { get; set; }

        public virtual ProductAttribute ProductAttribute { get; set; }
        public virtual ProductAttributeSet ProductAttributeSet { get; set; }
    }
}
