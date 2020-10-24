using eSuperShop.Repository;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public interface IStoreCore
    {
        DbResponse<List<StoreViewModel>> TopStores(StoreFilterRequest model);
    }
}