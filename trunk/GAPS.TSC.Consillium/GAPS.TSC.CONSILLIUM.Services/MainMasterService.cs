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
        IEnumerable<ClientModel> GetAllClients();

        IEnumerable<GeographyModel> GetAllGeographies();
        IEnumerable<Industry> GetAllIndustries();
        IEnumerable<CurrencyModel> GetAllCurrencies();
        IEnumerable<CountryModel> GetAllCountries();
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

        public IEnumerable<ClientModel> GetAllClients()
        {
            var str = RestService.Get<List<ClientModel>>("clients");
            
            if (str != null)
            {
                return str;
            }
            return new List<ClientModel>();
        }

        public IEnumerable<GeographyModel> GetAllGeographies()
        {
            var data = RestService.Get<List<GeographyModel>>("masters/geographies");
            if (data != null)
            {
              return  data.OrderBy(x => x.SequenceNo);
            }
            return new List<GeographyModel>();
        }
        public IEnumerable<Industry>GetAllIndustries()
        {
            
             var data = RestService.Get<List<Industry>>("masters/ClientIndustries");
            if (data != null)
            {
               return data.OrderBy(x => x.Name);
            }
            return new List<Industry>();
        }
        public IEnumerable<CurrencyModel> GetAllCurrencies()
        {
             var data =RestService.Get<List<CurrencyModel>>("masters/currencies");
            if (data!=null)
            {
                return data.OrderBy(x => x.SequenceNo);
            }
            return new List<CurrencyModel>();
        }

        public IEnumerable<CountryModel> GetAllCountries()
        {
            return RestService.Get<List<CountryModel>>("masters/countries");
        } 
    }
}