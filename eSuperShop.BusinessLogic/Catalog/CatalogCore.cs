using AutoMapper;
using eSuperShop.Data;
using eSuperShop.Repository;
using System;
using System.Collections.Generic;

namespace eSuperShop.BusinessLogic
{
    public class CatalogCore : ICatalogCore
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public CatalogCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public DbResponse<CatalogDisplayModel> Add(CatalogAddModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse<CatalogDisplayModel>(false, "Invalid User");

                model.CreatedByRegistrationId = registrationId;

                if (string.IsNullOrEmpty(model.CatalogName) && string.IsNullOrEmpty(model.SlugUrl))
                    return new DbResponse<CatalogDisplayModel>(false, "Invalid Data");

                if (_db.Catalog.IsExistName(model.CatalogName))
                    return new DbResponse<CatalogDisplayModel>(false, "Catalog Name already Exist", null, "CatalogName");

                if (_db.Catalog.IsExistSlugUrl(model.SlugUrl))
                    return new DbResponse<CatalogDisplayModel>(false, "SlugUrl already Exist", null, "SlugUrl");


                _db.Catalog.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<CatalogDisplayModel>(_db.Catalog.Catalog);

                return new DbResponse<CatalogDisplayModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<CatalogDisplayModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                if (_db.Catalog.IsIsNull(id))
                    return new DbResponse(false, "Data not found");
                if (_db.Catalog.IsRelatedDataExist(id))
                    return new DbResponse(false, "Related data Exist");

                _db.Catalog.Delete(id);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<List<CatalogDisplayModel>> Display(CatalogDisplayPlace place, int numberOfItem)
        {
            try
            {
                var data = _db.Catalog.Display(place, numberOfItem);
                return new DbResponse<List<CatalogDisplayModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<CatalogDisplayModel>>(false, e.Message);
            }
        }

        public DbResponse<List<CatalogModel>> List()
        {
            try
            {
                var data = _db.Catalog.List();
                return new DbResponse<List<CatalogModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<CatalogModel>>(false, e.Message);
            }
        }

        public DbResponse<string> Breadcrumb(int id)
        {
            try
            {
                var data = _db.Catalog.Breadcrumb(id);
                return new DbResponse<string>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<string>(false, e.Message);
            }
        }

        public List<DDL> DisplayPlaceDdl()
        {
            return _db.Catalog.SliderPlaceDdl();
        }

        public DbResponse<List<DDL>> ListDdl()
        {
            try
            {
                var data = _db.Catalog.ListDdl();
                return new DbResponse<List<DDL>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<DDL>>(false, e.Message);
            }
        }
    }
}