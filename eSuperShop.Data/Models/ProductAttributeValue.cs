using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class ProductAttributeValue
    {
        public ProductAttributeValue()
        {
            ProductQuantitySetAttribute = new HashSet<ProductQuantitySetAttribute>();
        }
        public int ProductAttributeValueId { get; set; }
        public int ProductAttributeId { get; set; }
        public string Value { get; set; }
        public string ImageUrl { get; set; }
        public DateTime InsertedOnUtc { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
        public virtual ICollection<ProductQuantitySetAttribute> ProductQuantitySetAttribute { get; set; }
    }
}