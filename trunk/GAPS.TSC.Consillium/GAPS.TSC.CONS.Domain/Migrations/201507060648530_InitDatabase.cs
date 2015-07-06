namespace GAPS.TSC.CONS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ActualName = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Calls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpertId = c.Int(nullable: false),
                        CallFacilitatedById = c.Int(nullable: false),
                        CallType = c.Int(nullable: false),
                        InterviewDate = c.DateTime(nullable: false),
                        CallDuration = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FeesPerHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostBorneBy = c.Int(nullable: false),
                        Details = c.String(),
                        GeographicId = c.Int(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountCurrencyId = c.Int(nullable: false),
                        PaymentStatus = c.Int(nullable: false),
                        PaymentInitiationDate = c.DateTime(),
                        PaymentConfirmationDate = c.DateTime(),
                        PaymentConfirmedById = c.Int(),
                        PaymentInitiatedById = c.Int(),
                        PaymentModeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamMembers", t => t.CallFacilitatedById, cascadeDelete: true)
                .ForeignKey("dbo.Experts", t => t.ExpertId, cascadeDelete: true)
                .ForeignKey("dbo.TeamMembers", t => t.PaymentConfirmedById)
                .ForeignKey("dbo.TeamMembers", t => t.PaymentInitiatedById)
                .ForeignKey("dbo.PaymentModes", t => t.PaymentModeId, cascadeDelete: true)
                .Index(t => t.ExpertId)
                .Index(t => t.CallFacilitatedById)
                .Index(t => t.PaymentConfirmedById)
                .Index(t => t.PaymentInitiatedById)
                .Index(t => t.PaymentModeId);
            
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        TeamMemberType = c.Int(nullable: false),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Experts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Name = c.String(),
                        CountryId = c.Int(),
                        City = c.String(),
                        Email = c.String(),
                        PrimaryContact = c.String(),
                        SecondaryContact = c.String(),
                        LinkedInUrl = c.String(),
                        ContactedOn = c.DateTime(),
                        FeesAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FeesCurrencyId = c.Int(nullable: false),
                        LeadStatus = c.Int(nullable: false),
                        LeadComments = c.String(),
                        GeographicId = c.Int(),
                        IndustryId = c.Int(),
                        RecruiterId = c.Int(nullable: false),
                        JobHistory = c.String(),
                        Source = c.String(),
                        Notes = c.String(),
                        ExpertType = c.Int(nullable: false),
                        JoiningDate = c.DateTime(),
                        ResumeId = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamMembers", t => t.RecruiterId)
                .ForeignKey("dbo.Attachments", t => t.ResumeId)
                .Index(t => t.RecruiterId)
                .Index(t => t.ResumeId);
            
            CreateTable(
                "dbo.ExpertNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpertId = c.Int(nullable: false),
                        TeamMemberId = c.Int(nullable: false),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Experts", t => t.ExpertId, cascadeDelete: true)
                .ForeignKey("dbo.TeamMembers", t => t.TeamMemberId, cascadeDelete: true)
                .Index(t => t.ExpertId)
                .Index(t => t.TeamMemberId);
            
            CreateTable(
                "dbo.ExpertRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(),
                        ProjectName = c.String(),
                        ClientName = c.String(),
                        Description = c.String(),
                        IndustryId = c.Int(nullable: false),
                        Comments = c.String(),
                        GeographicId = c.Int(nullable: false),
                        BudgetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetCurrencyId = c.Int(nullable: false),
                        CostSharingType = c.Int(nullable: false),
                        TscShare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientShare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ScopingDocumentId = c.Int(nullable: false),
                        ApprovalDocumentId = c.Int(nullable: false),
                        BdLeadId = c.Int(),
                        ProjectLeadId = c.Int(),
                        UnitId = c.Int(),
                        ExpertRequestType = c.Int(nullable: false),
                        RequestStatus = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        RestartDate = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkExperiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpertId = c.Int(nullable: false),
                        Designation = c.String(),
                        Organisation = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Experts", t => t.ExpertId, cascadeDelete: true)
                .Index(t => t.ExpertId);
            
            CreateTable(
                "dbo.PaymentModes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExpertRequestExperts",
                c => new
                    {
                        ExpertRequest_Id = c.Int(nullable: false),
                        Expert_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExpertRequest_Id, t.Expert_Id })
                .ForeignKey("dbo.ExpertRequests", t => t.ExpertRequest_Id, cascadeDelete: true)
                .ForeignKey("dbo.Experts", t => t.Expert_Id, cascadeDelete: true)
                .Index(t => t.ExpertRequest_Id)
                .Index(t => t.Expert_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Calls", "PaymentModeId", "dbo.PaymentModes");
            DropForeignKey("dbo.Calls", "PaymentInitiatedById", "dbo.TeamMembers");
            DropForeignKey("dbo.Calls", "PaymentConfirmedById", "dbo.TeamMembers");
            DropForeignKey("dbo.Calls", "ExpertId", "dbo.Experts");
            DropForeignKey("dbo.WorkExperiences", "ExpertId", "dbo.Experts");
            DropForeignKey("dbo.Experts", "ResumeId", "dbo.Attachments");
            DropForeignKey("dbo.Experts", "RecruiterId", "dbo.TeamMembers");
            DropForeignKey("dbo.ExpertRequestExperts", "Expert_Id", "dbo.Experts");
            DropForeignKey("dbo.ExpertRequestExperts", "ExpertRequest_Id", "dbo.ExpertRequests");
            DropForeignKey("dbo.ExpertNotes", "TeamMemberId", "dbo.TeamMembers");
            DropForeignKey("dbo.ExpertNotes", "ExpertId", "dbo.Experts");
            DropForeignKey("dbo.Calls", "CallFacilitatedById", "dbo.TeamMembers");
            DropIndex("dbo.ExpertRequestExperts", new[] { "Expert_Id" });
            DropIndex("dbo.ExpertRequestExperts", new[] { "ExpertRequest_Id" });
            DropIndex("dbo.WorkExperiences", new[] { "ExpertId" });
            DropIndex("dbo.ExpertNotes", new[] { "TeamMemberId" });
            DropIndex("dbo.ExpertNotes", new[] { "ExpertId" });
            DropIndex("dbo.Experts", new[] { "ResumeId" });
            DropIndex("dbo.Experts", new[] { "RecruiterId" });
            DropIndex("dbo.Calls", new[] { "PaymentModeId" });
            DropIndex("dbo.Calls", new[] { "PaymentInitiatedById" });
            DropIndex("dbo.Calls", new[] { "PaymentConfirmedById" });
            DropIndex("dbo.Calls", new[] { "CallFacilitatedById" });
            DropIndex("dbo.Calls", new[] { "ExpertId" });
            DropTable("dbo.ExpertRequestExperts");
            DropTable("dbo.PaymentModes");
            DropTable("dbo.WorkExperiences");
            DropTable("dbo.ExpertRequests");
            DropTable("dbo.ExpertNotes");
            DropTable("dbo.Experts");
            DropTable("dbo.TeamMembers");
            DropTable("dbo.Calls");
            DropTable("dbo.Attachments");
        }
    }
}
