using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository.Repositories
{
    public class RegionRepository : Repository, IRegionRepository
    {
        public RegionRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {

        }

        public DbResponse<RegionAddEditModel> Add(RegionAddEditModel model)
        {

            //try
            //{
            var region = _mapper.Map<Region>(model);
            Db.Region.Add(region);
            Db.SaveChanges();
            model.RegionId = region.RegionId;

            return new DbResponse<RegionAddEditModel>(true, $"{model.RegionName} Add Successfully", model);
            //}
            //catch (Exception e)
            //{
            //    return new DbResponse<RegionAddEditModel>(false, $"{e.Message}. {e.InnerException?.Message ?? ""}");
            //}


        }

        public DbResponse Edit(RegionAddEditModel model)
        {
            var region = Db.Region.Find(model.RegionId);
            region.RegionName = model.RegionName;
            Db.Region.Update(region);
            Db.SaveChanges();
            return new DbResponse(true, $"{region.RegionName} Updated Successfully");
        }

        public DbResponse Delete(int id)
        {
            var region = Db.Region.Find(id);
            Db.Region.Remove(region);
            Db.SaveChanges();
            return new DbResponse(true, $"{region.RegionName} Deleted Successfully");
        }

        public DbResponse<RegionAddEditModel> Get(int id)
        {
            var region = Db.Region.Where(r => r.RegionId == id)
                .ProjectTo<RegionAddEditModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
            return new DbResponse<RegionAddEditModel>(true, $"{region.RegionName} Get Successfully", region);
        }

        public bool IsExistName(string name)
        {
            return Db.Region.Any(r => r.RegionName == name);
        }

        public bool IsExistName(string name, int updateId)
        {
            return Db.Region.Any(r => r.RegionName == name && r.RegionId != updateId);
        }

        public bool IsNull(int id)
        {
            return Db.Region.Any(r => r.RegionId == id);
        }

        public bool IsRelatedDataExist(int id)
        {
            return false;
        }

        public DataResult<RegionAddEditModel> List(DataRequest request)
        {
            return Db.Region
                 .ProjectTo<RegionAddEditModel>(_mapper.ConfigurationProvider)
                 .ToDataResult(request);
        }

        public List<DDL> ListDdl()
        {
            return Db.Region
                .OrderBy(r => r.RegionName)
                .Select(r => new DDL
                {
                    value = r.RegionId.ToString(),
                    label = r.RegionName
                }).ToList();
        }
    }
}