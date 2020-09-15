﻿using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public interface ICatalogModel
    {
        int CatalogId { get; set; }
        string CatalogName { get; set; }
        string SlugUrl { get; set; }
        string ImageUrl { get; set; }
        bool IsExist { get; set; }
        IEnumerable<ICatalogModel> SubCatalog { get; set; }
    }
}