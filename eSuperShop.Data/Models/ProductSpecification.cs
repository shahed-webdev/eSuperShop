using System;

namespace eSuperShop.Data
{
    public class ProductSpecification
    {
        public int ProductSpecificationId { get; set; }
        public int ProductId { get; set; }
        public int SpecificationId { get; set; }
        public string Value { get; set; }
        public DateTime InsertedOnUtc { get; set; }

        public virtual Product Product { get; set; }
        public virtual AllSpecification Specification { get; set; }
    }
}
