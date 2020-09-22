namespace eSuperShop.Data
{
    public class ProductQuantitySetAttribute
    {
        public int ProductQuantitySetAttributeId { get; set; }
        public int ProductQuantitySetId { get; set; }
        public int ProductAttributeValueId { get; set; }

        public virtual ProductAttributeValue ProductAttributeValue { get; set; }
        public virtual ProductQuantitySet ProductQuantitySet { get; set; }
    }
}
