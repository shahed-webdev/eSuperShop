using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository.Repositories
{
    public class AreaRepository : Repository, IAreaRepository
    {
        public AreaRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {

        }


        public DbResponse<AreaAddEditModel> Add(AreaAddEditModel model)
        {
            var area = _mapper.Map<Area>(model);
            Db.Area.Add(area);
            Db.SaveChanges();
            model.AreaId = area.AreaId;

            return new DbResponse<AreaAddEditModel>(true, $"{model.AreaName} Added Successfully", model);
        }

        public DbResponse Edit(AreaAddEditModel model)
        {
            var area = Db.Area.Find(model.AreaId);
            area.RegionId = model.RegionId;
            area.AreaName = model.AreaName;
            Db.Area.Update(area);
            Db.SaveChanges();
            return new DbResponse(true, $"{area.AreaName} Updated Successfully");

        }

        public DbResponse Delete(int id)
        {
            var area = Db.Area.Find(id);
            Db.Area.Remove(area);
            Db.SaveChanges();
            return new DbResponse(true, $"{area.AreaName} Deleted Successfully");
        }

        public DbResponse<AreaAddEditModel> Get(int id)
        {
            var area = Db.Area.Where(r => r.AreaId == id)
                .ProjectTo<AreaAddEditModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
            return new DbResponse<AreaAddEditModel>(true, $"{area.RegionName} Get Successfully", area);
        }

        public bool IsExistName(string name, int regionId)
        {
            return Db.Area.Any(r => r.AreaName == name && r.RegionId == regionId);
        }

        public bool IsExistName(string name, int regionId, int updateId)
        {
            return Db.Area.Any(r => r.AreaName == name && r.RegionId == regionId && r.RegionId != updateId);
        }

        public bool IsNull(int id)
        {
            return !Db.Area.Any(r => r.AreaId == id);
        }

        public bool IsRelatedDataExist(int id)
        {
            return false;
        }

        public DataResult<AreaAddEditModel> List(DataRequest request)
        {
            return Db.Area
                .ProjectTo<AreaAddEditModel>(_mapper.ConfigurationProvider)
                .OrderBy(a => a.RegionName)
                .ThenBy(a => a.AreaName)
                .ToDataResult(request);
        }
        public List<DDL> ListDdl()
        {
            return Db.Area
                .OrderBy(r => r.AreaName)
                .Select(r => new DDL
                {
                    value = r.AreaId.ToString(),
                    label = r.AreaName
                }).ToList();
        }
        public List<DDL> ListDdlRegionWise(int regionId)
        {
            return Db.Area
                .Where(r => r.RegionId == regionId)
                .OrderBy(r => r.AreaName)
                .Select(r => new DDL
                {
                    value = r.AreaId.ToString(),
                    label = r.AreaName
                }).ToList();
        }
        public List<AreaAddEditModel> GetRegionWiseArea(int regionId)
        {
            return Db.Area
                .Where(r => r.RegionId == regionId)
                .OrderBy(r => r.AreaName)
                .ProjectTo<AreaAddEditModel>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}