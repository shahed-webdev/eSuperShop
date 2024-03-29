﻿using System;

namespace eSuperShop.Data
{
    public class Seo
    {
        public int SeoId { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedByRegistrationId { get; set; }

        public virtual Registration CreatedByRegistration { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual Product Product { get; set; }
    }
}
