using AutoMapper;
using eSuperShop.Repository;
using System;

namespace eSuperShop.BusinessLogic
{
    public class SeoCoreProduct : ISeoCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public SeoCoreProduct(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public DbResponse<SeoModel> Get(int productId)
        {
            try
            {
                if (_db.Product.IsNull(productId))
                    return new DbResponse<SeoModel>(false, "Data not found");

                var data = _db.Product.GetSeo(productId);

                if (data.SeoId == 0)
                    return new DbResponse<SeoModel>(false, "Seo not found");

                return new DbResponse<SeoModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<SeoModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int productId)
        {
            try
            {
                if (_db.Product.IsNull(productId))
                    return new DbResponse(false, "Data not found");
                if (!_db.Product.IsSeoExist(productId))
                    return new DbResponse(false, "No SEO data found");

                _db.Product.SeoDelete(productId);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse Post(SeoAddModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse(false, "Invalid User");

                model.CreatedByRegistrationId = registrationId;
                if (_db.Product.IsNull(model.AssignTableId))
                    return new DbResponse(false, "Data not found");

                _db.Product.PostSeo(model);
                _db.SaveChanges();
                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }


    }
}