using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain.ApiModels {
    public class UserModel {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Code { get; set; }
        public string Gender { get; set; }

        public DateTime? Dob { get; set; }

        public string OfficialEmail { get; set; }

        public DateTime? DateOfJoining { get; set; }

        public int? LocationId { get; set; }
        public int? GroupId { get; set; }
        public int? UnitId { get; set; }
        public int? ReportingManagerId { get; set; }
        public int? GroupHeadId { get; set; }
        public int? UnitHeadId { get; set; }
        public bool? IsActive { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        public int? DesignationId { get; set; }
    }
    public class GroupModel {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int Id { get; set; }
        public int GroupHeadId { get; set; }
    }
    public class UnitModel {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int UnitHeadId { get; set; }
        public string Code { get; set; }
    }

    public class CurrencyModel {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public int SequenceNo { get; set; }
        public bool IsActive { get; set; }
    }

    public class GeographyModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SequenceNo { get; set; }
        public bool IsActive { get; set; }
    }
    public class Industry {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class CountryModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
