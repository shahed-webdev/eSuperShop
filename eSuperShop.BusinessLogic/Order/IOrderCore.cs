using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;

namespace eSuperShop.BusinessLogic
{
    public interface IOrderCore
    {
        DbResponse<int> OrderPlace(OrderPlaceModel model, string customerUserName);

        DbResponse<DataResult<OrderAdminWiseListModel>> AdminWiseList(DataRequest request);
    }
}