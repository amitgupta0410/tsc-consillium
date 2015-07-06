using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain
{

    public enum TeamMemberType
    {
        Internal,
        External
    }
    public enum ActivityType
    {
        Editing,
        Training,
        Review,
        Others,
        Bench
    }

    public enum ProjectStatus
    {
        [Display(Name = "Not Started")]
        NotStarted,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "On Hold")]
        OnHold,
        [Display(Name = "In Pipeline")]
        InPipeline,
        [Display(Name = "Delivered")]
        Delivered,
        [Display(Name = "Scrapped")]
        Scrapped,
        [Display(Name = "No Communication")]
        NoCommunication,
        [Display(Name = "Dismiss Approved")]
        DismissApproved,
        [Display(Name = "Dismiss Rejected")]
        DismissRejected,
        [Display(Name = "Dismiss Requested")]
        DismissRequested
    }

    public enum ReviewStatus
    {
        [Display(Name = "Not Started")]
        NotStarted,
        [Display(Name = "In Pipeline")]
        InPipeline,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "On Hold")]
        OnHold,
        [Display(Name = "Review Over")]
        ReviewOver,
        [Display(Name = "Scrapped")]
        Scrapped
    }

    public enum MessageType
    {
        Success,
        Info,
        Danger
    }

    public enum Messages
    {
        Random,
        RequestError,
        RequestSuccess,
        UpdateTimesheet,
        ErrorInTimesheet,
        AddTimesheet,
        DeleteTimesheet,
        NotMoreThanEndDate,
        ProjectAdded,
        ProjectUpdated,
        Duplicate
    }
    public enum ProjectStaffType
    {
        SuperPL = 1,
        PL,
        Analyst,
        Reviewer,
        Developer,
        Tester
    }
}
