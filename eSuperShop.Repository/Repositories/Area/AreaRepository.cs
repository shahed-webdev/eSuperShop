using AutoMapper;
using eSuperShop.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace eSuperShop.Repository.Repositories
{
    public class AreaRepository : Repository, IAreaRepository
    {
        public AreaRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {

        }

        public Area Area { get; set; }
        public void Add(AreaAddEditModel model)
        {
            Area = _mapper.Map<Area>(model);
            Db.Area.Add(Area);
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public AreaAddEditModel Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistName(string name)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistName(string name, int updateId)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNull(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool IsRelatedDataExist(int id)
        {
            throw new System.NotImplementedException();
        }

        public DataResult<AreaAddEditModel> List(DataRequest request)
        {
            throw new System.NotImplementedException();
        }

        public List<DDL> ListDdl()
        {
            throw new System.NotImplementedException();
        }

        public List<AreaAddEditModel> GetRegionWiseArea(List<int> regionIds)
        {
            throw new System.NotImplementedException();
        }
    }
}