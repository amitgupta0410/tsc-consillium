using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Models
{
    public class AddProjectLeadModel
    {
        public AddProjectLeadModel()
        {
            ProjectLead = new List<SpecialProjectLeadMap>();
        }

        public int Id { get; set; }
        public String Name { get; set; }
        public bool Status { get; set; }
        public IEnumerable<SpecialProjectLeadMap>ProjectLead { get; set; }
    }

}