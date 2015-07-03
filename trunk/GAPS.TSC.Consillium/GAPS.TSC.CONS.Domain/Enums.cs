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
        Both,
        ManDayBilling,
    }

    public enum LeadStatus {
        New,
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
    public enum ProjectStaffType {
        SuperPL = 1,
        PL,
        Analyst,
        Reviewer,
        Developer,
        Tester
    }

}
