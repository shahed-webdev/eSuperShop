using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class ProductAttributeSet
    {
        public ProductAttributeSet()
        {
            ProductAttributeSetList = new HashSet<ProductAttributeSetList>();
        }

        public int ProductAttributeSetId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAdjustment { get; set; }
        public DateTime InsertedOnUtc { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<ProductAttributeSetList> ProductAttributeSetList { get; set; }
    }
}
