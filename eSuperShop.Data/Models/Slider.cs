using System;

namespace eSuperShop.Data
{
    public class Slider
    {
        public int SliderId { get; set; }
        public string ImageFileName { get; set; }
        public string RedirectUrl { get; set; }
        public int? DisplayOrder { get; set; }
        public SliderDisplayPlace DisplayPlace { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedByRegistrationId { get; set; }

        public virtual Registration CreatedByRegistration { get; set; }
    }
}
