using AutoMapper;
using eSuperShop.Repository;
using System;

namespace eSuperShop.BusinessLogic
{
    public class SeoCoreCatalog : ISeoCore
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public SeoCoreCatalog(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }
        public DbResponse<SeoModel> Get(int id)
        {
            try
            {
                if (_db.Catalog.IsNull(id))
                    return new DbResponse<SeoModel>(false, "Data not found");

                var data = _db.Catalog.GetSeo(id);

                return new DbResponse<SeoModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<SeoModel>(false, e.Message);
            }
        }
        public DbResponse Delete(int id)
        {
            try
            {
                if (_db.Catalog.IsNull(id))
                    return new DbResponse(false, "Data not found");
                if (!_db.Catalog.IsSeoExist(id))
                    return new DbResponse(false, "No SEO data found");

                _db.Catalog.SeoDelete(id);
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
                if (_db.Catalog.IsNull(model.AssignTableId))
                    return new DbResponse(false, "Data not found");

                _db.Catalog.PostSeo(model);
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