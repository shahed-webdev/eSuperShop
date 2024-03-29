﻿using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class ProductAttribute
    {
        public ProductAttribute()
        {
            ProductAttributeValue = new HashSet<ProductAttributeValue>();
        }

        public int ProductAttributeId { get; set; }
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public ProductAttributeDisplay DisplayType { get; set; }
        public DateTime InsertedOnUtc { get; set; }
        public virtual AllAttribute Attribute { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValue { get; set; }
    }
}
