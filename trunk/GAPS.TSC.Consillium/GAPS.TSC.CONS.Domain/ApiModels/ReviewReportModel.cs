using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace GAPS.TSC.CONS.Domain.ApiModels
{
    public class ReviewReportModel
    {
        public string ProjectDeliveryMonth { get; set; }
        public string Editors { get; set; }
        public string Reviewers { get; set; }
        public string Client { get; set; }
        public string ProjectLead { get; set; }
        public string Project { get; set; }
        public string MandatoryReview { get; set; }
        public int FeedbackRating { get; set; }
        public int Slides { get; set; }
        public decimal TimeSpent { get; set; }
    }

    public sealed class ReviewViewMap : CsvClassMap<ReviewReportModel>
    {
        public ReviewViewMap()
        {
            Map(x => x.ProjectDeliveryMonth).Name("Project Delivery Month");
            Map(x => x.Editors);
            Map(x => x.Reviewers).Name("Co-Editor or Reviewer");
            Map(x => x.Client);
            Map(x => x.MandatoryReview).Name("Mandatory Review");
            Map(x => x.ProjectLead).Name("Project Lead");
            Map(x => x.Project);
            Map(x => x.FeedbackRating).Name("Feedback Rating");
            Map(x => x.Slides).Name("Number of slides/pages");
            Map(x => x.TimeSpent).Name("Total Editorial Time Spent(Hrs)");

        }
    }

}
