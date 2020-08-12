using System;

namespace eSuperShop.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRegistrationRepository Registration { get; }
        ISliderRepository Slider { get; }
        int SaveChanges();
    }
}