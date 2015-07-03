using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain.ApiModels
{
    public class Project : IEquatable<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public string TypeDescription { get; set; }
        public int? CategoryId { get; set; }
        public string Category { get; set; }

        public string ProjectDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? InternalEndDate { get; set; }
        public string GroupForwardComment { get; set; }
        public string UnitForwardComment { get; set; }
        public string TeamStaffingComment { get; set; }

        public decimal? EstimatedEffort { get; set; }
        public int? PrimaryHours { get; set; }
        public decimal? LinguistAmount { get; set; }

        public decimal? DatabaseAmount { get; set; }

        public decimal? ExpertAmount { get; set; }

        public bool? IsCaseStudy { get; set; }
        public string ProjectWorkPath { get; set; }
        public int ProjectStatusId { get; set; }
        public string ProjectStatus { get; set; }
        public bool IsReview { get; set; }
        public bool IsAlive { get; set; }

        public DateTime? AddedOn { get; set; }

        public DateTime? LastUpdatedOn { get; set; }
        public bool? IsProjectAcceptance { get; set; }
        public bool? IsProjectReOpened { get; set; }
        public int? PayedDays { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public bool IsActive { get; set; }

        public int? ClientId { get; set; }
        public int? GroupId { get; set; }
        public int? UnitId { get; set; }
        /*    public int RequesterId { get; set; }
            public int? BackupRequesterId { get; set; }
            public int? LinguistAmountCurrencyId { get; set; }
            public int? DatabaseAmountCurrencyId { get; set; }
            public int? ExpertAmountCurrencyId { get; set; }
            public int AddedById { get; set; }
            public int LastUpdatedById { get; set; }*/
        public DateTime? ExpectedReviewDate { get; set; }
        public DateTime? InterimDeliveryDate { get; set; }
        public int? Slides { get; set; }
        public string Style { get; set; }
        public int? QmReviewerId { get; set; }
        public string OutputType { get; set; }
        public bool Equals(Project other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Project)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    public class ClientModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
    public class ProjectLeadModel
    {
        public int EmployeeId { get; set; }
        public bool IsActive { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

    }
}
