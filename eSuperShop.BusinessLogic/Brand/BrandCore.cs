using AutoMapper;
using eSuperShop.Repository;
using JqueryDataTables.LoopsIT;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eSuperShop.BusinessLogic
{
    public class BrandCore : IBrandCore
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;

        public BrandCore(IMapper mapper, IUnitOfWork db)
        {
            _mapper = mapper;
            _db = db;
        }
        public DbResponse<BrandModel> Add(BrandAddModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse<BrandModel>(false, "Invalid User");

                model.CreatedByRegistrationId = registrationId;

                if (string.IsNullOrEmpty(model.Name))
                    return new DbResponse<BrandModel>(false, "Invalid Data");

                if (_db.Brand.IsExistName(model.Name))
                    return new DbResponse<BrandModel>(false, "Brand Name already Exist", null, "Name");

                _db.Brand.Add(model);
                _db.SaveChanges();

                var data = _mapper.Map<BrandModel>(_db.Brand.Brand);

                return new DbResponse<BrandModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<BrandModel>(false, e.Message);
            }
        }

        public DbResponse<BrandModel> Get(int id)
        {
            try
            {
                if (_db.Brand.IsNull(id))
                    return new DbResponse<BrandModel>(false, "Data not found");

                var data = _db.Brand.Get(id);
                return new DbResponse<BrandModel>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<BrandModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                if (_db.Brand.IsNull(id))
                    return new DbResponse(false, "Data not found");
                if (_db.Brand.IsRelatedDataExist(id))
                    return new DbResponse(false, "Related data Exist");

                _db.Brand.Delete(id);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse Edit(BrandEditModel model)
        {
            try
            {
                if (_db.Brand.IsNull(model.BrandId))
                    return new DbResponse(false, "Data not found");
                if (string.IsNullOrEmpty(model.Name))
                    return new DbResponse(false, "Invalid Data");
                if (_db.Brand.IsExistName(model.Name, model.BrandId))
                    return new DbResponse(false, "Brand Name already Exist");

                _db.Brand.Edit(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse AssignCatalog(BrandAssignModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse(false, "Invalid User");
                model.AssignedByRegistrationId = registrationId;

                if (_db.Brand.IsExistBrandInCatalog(model.BrandId, model.CatalogId)) return new DbResponse(false, "Already Assigned");
                _db.Brand.AssignCatalog(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse AssignCatalogMultiple(BrandAssignMultipleModel model, string userName)
        {
            try
            {
                var registrationId = _db.Registration.GetRegID_ByUserName(userName);
                if (registrationId == 0) return new DbResponse(false, "Invalid User");
                model.AssignedByRegistrationId = registrationId;

                _db.Brand.AssignCatalogMultiple(model);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse UnAssignCatalog(int brandId, int catalogId)
        {
            try
            {
                if (!_db.Brand.IsExistBrandInCatalog(brandId, catalogId)) return new DbResponse(false, "Data Not Found");
                _db.Brand.UnAssignCatalog(brandId, catalogId);
                _db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public Task<ICollection<BrandModel>> SearchAsync(string key)
        {
            return _db.Brand.SearchAsync(key);
        }

        public DataResult<BrandModel> List(DataRequest request)
        {
            return _db.Brand.List(request);
        }

        public DbResponse<List<DDL>> Ddl()
        {
            try
            {
                var data = _db.Brand.ListDdl();
                return new DbResponse<List<DDL>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<DDL>>(false, e.Message);
            }
        }

        public DbResponse<List<BrandModel>> CatalogWiseList(int catalogId)
        {
            try
            {
                var data = _db.Brand.CatalogWiseList(catalogId);
                return new DbResponse<List<BrandModel>>(true, "Success", data);
            }
            catch (Exception e)
            {
                return new DbResponse<List<BrandModel>>(false, e.Message);
            }
        }
    }
}