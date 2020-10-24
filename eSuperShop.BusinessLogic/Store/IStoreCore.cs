using eSuperShop.Repository;
using Paging.Infrastructure;

namespace eSuperShop.BusinessLogic
{
    public interface IStoreCore
    {
        DbResponse<PagedResult<StoreViewModel>> TopStores(StoreFilterRequest model);
    }
}