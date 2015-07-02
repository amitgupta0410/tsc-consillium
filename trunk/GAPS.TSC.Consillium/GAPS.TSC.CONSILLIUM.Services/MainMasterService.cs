using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain.ApiModels;

namespace GAPS.TSC.CONSILLIUM.Services
{
    public interface IMainMastersService
    {
        IEnumerable<GroupModel> GetAllGroups();
        IQueryable<GroupModel> FindGroups(Expression<Func<GroupModel, bool>> filter);
        GroupModel FindGroupById(int id);


        IEnumerable<UnitModel> GetAllUnits();
        IQueryable<UnitModel> FindUnits(Expression<Func<UnitModel, bool>> filter);
        UnitModel FindUnitById(int id);
    }
    public class MainMastersService : IMainMastersService
    {
        private const string GroupCacheKey = "Groups";
        private const string UnitCacheKey = "Units";
        private const string GeographyCacheKey = "Geographies";

        public MainMastersService()
        {
        }

        public IEnumerable<GroupModel> GetAllGroups()
        {

            if (!CacheHelper.HasKey(GroupCacheKey))
            {
                CacheHelper.Add(GroupCacheKey, RestService.Get<List<GroupModel>>("masters/groups"));
            }

            var str = CacheHelper.Get(GroupCacheKey) as List<GroupModel>;
            if (str != null)
            {
                return str.Where(x => x.IsActive.HasValue && x.IsActive.Value);
            }
            return new List<GroupModel>();

        }

        public IQueryable<GroupModel> FindGroups(Expression<Func<GroupModel, bool>> filter)
        {
            return GetAllGroups().AsQueryable().Where(filter);
        }

        public GroupModel FindGroupById(int id)
        {
            return GetAllGroups().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<UnitModel> GetAllUnits()
        {

            if (!CacheHelper.HasKey(UnitCacheKey))
            {
                CacheHelper.Add(UnitCacheKey, RestService.Get<List<UnitModel>>("masters/units"));
            }

            var str = CacheHelper.Get(UnitCacheKey) as List<UnitModel>;
            if (str != null)
            {
                return str.Where(x => x.IsActive.HasValue && x.IsActive.Value);
            }
            return new List<UnitModel>();

        }

        public IQueryable<UnitModel> FindUnits(Expression<Func<UnitModel, bool>> filter)
        {
            return GetAllUnits().AsQueryable().Where(filter);
        }

        public UnitModel FindUnitById(int id)
        {
            return GetAllUnits().FirstOrDefault(x => x.Id == id);
        }



    }
}
