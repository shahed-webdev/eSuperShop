using AutoMapper;
using AutoMapper.QueryableExtensions;
using eSuperShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eSuperShop.Repository
{
    public class SliderRepository : Repository, ISliderRepository
    {

        public SliderRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public Slider Slider { get; set; }

        public void Add(SliderAddModel model)
        {
            Slider = _mapper.Map<Slider>(model);
            Db.Slider.Add(Slider);
        }

        public void Delete(int id)
        {
            Slider = Db.Slider.Find(id);
            Db.Slider.Remove(Slider);
        }

        public bool IsNull(int id)
        {
            return Db.Slider.Any(s => s.SliderId == id);
        }

        public List<SliderSlideModel> Display(SliderDisplayPlace place)
        {
            return Db.Slider.Where(s => s.DisplayPlace == place).ProjectTo<SliderSlideModel>(_mapper.ConfigurationProvider).ToList();
        }

        public List<SliderListModel> List()
        {
            return Db.Slider.ProjectTo<SliderListModel>(_mapper.ConfigurationProvider).ToList();
        }

        public List<DDL> SliderPlaceDdl()
        {
            var list = from SliderDisplayPlace a in Enum.GetValues(typeof(SliderDisplayPlace))
                       select
                           new DDL
                           {
                               label = a.ToString(),
                               value = (int)a
                           };
            return list.ToList();
        }
    }
}