﻿namespace eSuperShop.Repository
{
    public class VendorInfoUpdateByAdminModel
    {
        public int VendorId { get; set; }
        public string AuthorizedPerson { get; set; }
        public string StoreName { get; set; }
        public string StoreSlugUrl { get; set; }
        public string StorePostcode { get; set; }
        public int? StoreAreaId { get; set; }

    }
}