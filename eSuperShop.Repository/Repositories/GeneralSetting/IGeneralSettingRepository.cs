namespace eSuperShop.Repository
{
    public interface IGeneralSettingRepository
    {
        DbResponse ChangeOrderQuantityLimit(int quantity);
        DbResponse<int> GetOrderQuantityLimit();
    }
}