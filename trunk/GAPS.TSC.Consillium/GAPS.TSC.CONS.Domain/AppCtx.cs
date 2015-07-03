using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain
{
    public class AppCtx : DbContext
    {
        public AppCtx() : base("AppCtx") { }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<ExpertNote> ExpertNotes { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<ExpertRequest> ExpertRequests { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

//            modelBuilder.Entity<Resume>().HasRequired(x => x.Profile).WithMany(x => x.Resumes).HasForeignKey(x => x.ProfileId).WillCascadeOnDelete(false);
//            modelBuilder.Entity<OfferDocument>().HasRequired(x => x.Offer).WithMany(x => x.OfferDocuments).HasForeignKey(x => x.OfferId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Expert>()
                .HasRequired(x => x.Recruiter).WithMany().HasForeignKey(x=>x.RecruiterId).WillCascadeOnDelete(false);


       
            base.OnModelCreating(modelBuilder);

        }

    }
}
