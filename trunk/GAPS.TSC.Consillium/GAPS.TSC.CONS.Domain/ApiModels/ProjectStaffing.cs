using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain.ApiModels
{
    public class ProjectStaffing
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int StaffTypeId { get; set; }
        public int PerAllocation { get; set; }
        public DateTime StaffingStartDate { get; set; }
        public DateTime StaffingEndDate { get; set; }
        public string StaffingComment { get; set; }
        public string AvailiblityComment { get; set; }
        public bool IsActive { get; set; }
        public int LastUpdatedById { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool IsRightsAssigned { get; set; }
        public int? FTECode { get; set; }
    }
}
