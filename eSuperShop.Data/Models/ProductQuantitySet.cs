﻿using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class ProductQuantitySet
    {
        public ProductQuantitySet()
        {
            ProductQuantitySetAttribute = new HashSet<ProductQuantitySetAttribute>();
            OrderCart = new HashSet<OrderCart>();
        }

        public int ProductQuantitySetId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAdjustment { get; set; }
        public DateTime InsertedOnUtc { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<ProductQuantitySetAttribute> ProductQuantitySetAttribute { get; set; }
        public virtual ICollection<OrderList> OrderList { get; set; }
        public virtual ICollection<OrderCart> OrderCart { get; set; }
    }
}
