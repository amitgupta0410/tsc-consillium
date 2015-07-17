using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain {


    public enum CostSharingType {
        TSC,
        Client,
        [Display(Name = "TSC+Client")]
        Both,
        [Display(Name = "Man Day Billing")]
        ManDayBilling,
    }

    public enum LeadStatus {
        New,
    }

    public enum TitleOptions
    {
        Mr,
        Mrs,
        Miss
    }

    public enum ExpertType {
        Strategic,
        OnProject
    }

    public enum PaymentStatus {
        InProgress,
        OnHold,
        Paid,
        Cancelled,
        WaitingForInvoice
    }

    public enum CallType {
        Call,
        Email,
        Survey,
    }

    public enum RequestStatus {
        NotStarted,
        Active,
        OnHold,
        Closed,
        Reopened,
    }

    public enum ExpertRequestType {
        En,
        Manual,
    }

    public enum TeamMemberType {
        Internal,
        External
    }


    public enum MessageType {
        Success,
        Info,
        Danger
    }

    public enum Messages
    {
        Random,
        RequestError,
        RequestSuccess,
        Duplicate,
        Update,
        AddLeadSuccess,
        ConvertLead,
        DeleteLead,
        AddCvSuccess,
        AddNoteSuccess,
        Danger
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
