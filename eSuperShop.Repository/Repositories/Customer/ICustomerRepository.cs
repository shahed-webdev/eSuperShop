namespace eSuperShop.Repository
{
    public interface ICustomerRepository
    {
        void Add(CustomerAddModel model);
        CustomerinfoModel Get(int customerId);
        CustomerDashboardModel Dashboard(int customerId);
    }
}