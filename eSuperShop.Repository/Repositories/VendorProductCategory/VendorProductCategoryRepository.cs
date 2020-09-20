using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class VendorProductCategoryRepository : Repository, IVendorProductCategoryRepository
    {
        public VendorProductCategoryRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public VendorProductCategory VendorProductCategory { get; set; }
        public VendorProductCategoryList VendorProductCategoryList { get; set; }
        public void Add(VendorProductCategoryAddModel model)
        {
            VendorProductCategory = _mapper.Map<VendorProductCategory>(model);
            Db.VendorProductCategory.Add(VendorProductCategory);
        }

        public void Delete(int vendorProductCategoryId)
        {
            VendorProductCategory = Db.VendorProductCategory.Find(vendorProductCategoryId);
            Db.VendorProductCategory.Remove(VendorProductCategory);
        }

        public void Update(VendorProductCategoryModel model)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistName(string name)
        {
            return Db.VendorProductCategory.Any(c => c.Name == name);
        }

        public bool IsExistName(string name, int updateId)
        {
            return Db.VendorProductCategory.Any(c => c.Name == name && c.VendorProductCategoryId != updateId);
        }

        public bool IsNull(int vendorProductCategoryId)
        {
            return !Db.VendorProductCategory.Any(c => c.VendorProductCategoryId == vendorProductCategoryId);
        }

        public bool IsRelatedDataExist(int id)
        {
            return Db.VendorProductCategoryList.Any(c => c.VendorProductCategoryId == id);
        }

        public List<VendorProductCategoryDisplayModel> DisplayList(int vendorId)
        {
            return Db.VendorProductCategory
                .Where(c => c.VendorId == vendorId)
                .ProjectTo<VendorProductCategoryDisplayModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public List<DDL> ListDdl(int vendorId)
        {
            var ddl = Db.VendorProductCategory
                .ToList().OrderBy(c => c.DisplayOrder).ThenBy(c => c.Name)
                .Select(c => new DDL
                {
                    value = c.VendorProductCategoryId.ToString(),
                    label = c.Name
                });

            return ddl?.ToList() ?? new List<DDL>();
        }

        public VendorProductCategoryModel Get(int id)
        {
            return Db.VendorProductCategory.ProjectTo<VendorProductCategoryModel>(_mapper.ConfigurationProvider).FirstOrDefault(c => c.VendorProductCategoryId == id);
        }

        public void PlaceAssign(VendorProductCategoryAssignModel model)
        {
            if (!IsPlaceAssign(model))
            {

                VendorProductCategoryList = _mapper.Map<VendorProductCategoryList>(model);
                Db.VendorProductCategoryList.Add(VendorProductCategoryList);
            }
        }

        public bool IsPlaceAssign(VendorProductCategoryAssignModel model)
        {
            VendorProductCategoryList = Db.VendorProductCategoryList.FirstOrDefault(c => c.VendorProductCategoryId == model.VendorProductCategoryId && c.ProductId == model.ProductId);
            return VendorProductCategoryList != null;
        }

        public void PlaceDelete(VendorProductCategoryAssignModel model)
        {
            VendorProductCategoryList = Db.VendorProductCategoryList.FirstOrDefault(c => c.VendorProductCategoryId == model.VendorProductCategoryId && c.ProductId == model.ProductId);
            Db.VendorProductCategoryList.Remove(VendorProductCategoryList);
        }

        public List<VendorCategoryProductsModel> Products(int vendorProductCategoryId)
        {
            throw new System.NotImplementedException();
        }
    }
}