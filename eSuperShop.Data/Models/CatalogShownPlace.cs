﻿using System;

namespace eSuperShop.Data
{
    public class CatalogShownPlace
    {
        public int CatalogShownPlaceId { get; set; }
        public int CatalogId { get; set; }
        public CatalogDisplayPlace ShownPlace { get; set; }
        public int? DisplayOrder { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedByRegistrationId { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual Registration CreatedByRegistration { get; set; }
    }
}
