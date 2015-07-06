using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GAPS.TSC.CONS.Domain.ApiModels;

namespace GAPS.TSC.CONS.Services{
    public interface IMainMastersService{
        IEnumerable<GroupModel> GetAllGroups();
        IQueryable<GroupModel> FindGroups(Expression<Func<GroupModel, bool>> filter);
        GroupModel FindGroupById(int id);


        IEnumerable<UnitModel> GetAllUnits();
        IQueryable<UnitModel> FindUnits(Expression<Func<UnitModel, bool>> filter);
        UnitModel FindUnitById(int id);
    }

    public class MainMastersService : IMainMastersService{
        public MainMastersService() {}

        public IEnumerable<GroupModel> GetAllGroups() {
            return RestService.Get<List<GroupModel>>("masters/groups");
        }

        public IQueryable<GroupModel> FindGroups(Expression<Func<GroupModel, bool>> filter) {
            return GetAllGroups().AsQueryable().Where(filter);
        }

        public GroupModel FindGroupById(int id) {
            return GetAllGroups().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<UnitModel> GetAllUnits() {
            return RestService.Get<List<UnitModel>>("masters/units");
        }

        public IQueryable<UnitModel> FindUnits(Expression<Func<UnitModel, bool>> filter) {
            return GetAllUnits().AsQueryable().Where(filter);
        }

        public UnitModel FindUnitById(int id) {
            return GetAllUnits().FirstOrDefault(x => x.Id == id);
        }
    }
}