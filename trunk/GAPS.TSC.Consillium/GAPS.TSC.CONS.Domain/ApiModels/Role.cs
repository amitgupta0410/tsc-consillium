using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;


namespace GAPS.TSC.CONS.Domain.ApiModels
{
    public class Role : IdentityRole, IEntity<string>
    {
        public const string Admin = "Admin";

        public const string ConsUser = "ConsUser";
      

        public const string ProjectLead = "ProjectLead";
        public const string GroupUnitHead = "GroupUnitHead";

    }

    public class Designation
    {

        public const int AssociateEditor = 109;
        public const int SeniorAssociateEditor = 19;
        public const int Editor = 8;
        public const int SeniorEditor = 72;

        public const int AssistantManager = 5;
        public const int AssistantManagerIt = 6;
        public const int AVP = 7;
        public const int Manager = 12;
        public const int SeniorManager = 21;
        public const int VP = 35;

        public static readonly List<int> EtTeam = new List<int> {
            AssociateEditor,SeniorAssociateEditor,SeniorEditor,Editor
        };

        public static readonly List<int> CanRequestReview = new List<int> {
            AssistantManager,AssistantManagerIt,AVP,Manager,SeniorManager,VP
        };
    }
}
