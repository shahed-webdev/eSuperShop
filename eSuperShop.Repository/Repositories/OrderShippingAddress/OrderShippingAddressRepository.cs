using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class OrderShippingAddressRepository : Repository, IOrderShippingAddressRepository
    {
        public OrderShippingAddressRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public OrderShippingAddress OrderShippingAddress { get; set; }

        public void Add(OrderShippingAddressAddModel model)
        {
            var address = _mapper.Map<OrderShippingAddress>(model);
            Db.OrderShippingAddress.Add(address);
        }
    }
}