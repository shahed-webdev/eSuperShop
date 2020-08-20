using System.Collections.Generic;
using AutoMapper;
using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public class CatalogRepository : Repository, ICatalogRepository
    {
        public CatalogRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }


        public Catalog Catalog { get; set; }
        public void Add(CatalogAddModel model)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistSlugUrl(string slugUrl)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistSlugUrl(string slugUrl, int updateId)
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

        public bool IsIsNull(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<CatalogDisplayModel> Display(CatalogDisplayPlace place, int numberOfItem)
        {
            throw new System.NotImplementedException();
        }

        public List<CatalogDisplayModel> DisplaySubCatalog(int parentCatalogId, int numberOfItem)
        {
            throw new System.NotImplementedException();
        }

        public List<SliderListModel> List()
        {
            throw new System.NotImplementedException();
        }

        public List<DDL> SliderPlaceDdl()
        {
            throw new System.NotImplementedException();
        }
    }
}