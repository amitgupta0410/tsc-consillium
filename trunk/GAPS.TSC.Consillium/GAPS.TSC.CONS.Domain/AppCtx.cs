using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain{
    public class AppCtx : DbContext{
        public AppCtx() : base("AppCtx") {}
        public DbSet<SpecialProjectLeadMap> ProjectLeads { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<ExpertNote> ExpertNotes { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<ExpertRequest> ExpertRequests { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ExpertRequestScopingDocumentMap> ExpertRequestScopingDocuments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            /*      modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
*/

            modelBuilder.Entity<Expert>()
                .HasRequired(x => x.Recruiter).WithMany().WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges() {
            var changeSet = ChangeTracker.Entries<ITrackable>();

            if (changeSet != null) {
                foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged)) {
                    if (entry.State == EntityState.Added) {
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                    } else if (entry.State == EntityState.Deleted) {
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        entry.State = EntityState.Modified;
                    }
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}