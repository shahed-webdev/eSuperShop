using System;

namespace eSuperShop.Data
{
    public partial class Slider
    {
        public int SliderId { get; set; }
        public string ImageUrl { get; set; }
        public int? DisplayOrder { get; set; }
        public string ShownPlace { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedByRegistrationId { get; set; }

        public virtual Registration CreatedByRegistration { get; set; }
    }
}
