namespace GAPS.TSC.CONS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScopingDocuments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachments", "ExpertRequest_Id", c => c.Int());
            CreateIndex("dbo.Attachments", "ExpertRequest_Id");
            AddForeignKey("dbo.Attachments", "ExpertRequest_Id", "dbo.ExpertRequests", "Id");
            DropColumn("dbo.ExpertRequests", "ScopingDocumentId");
            DropColumn("dbo.ExpertRequests", "ApprovalDocumentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExpertRequests", "ApprovalDocumentId", c => c.Int(nullable: false));
            AddColumn("dbo.ExpertRequests", "ScopingDocumentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Attachments", "ExpertRequest_Id", "dbo.ExpertRequests");
            DropIndex("dbo.Attachments", new[] { "ExpertRequest_Id" });
            DropColumn("dbo.Attachments", "ExpertRequest_Id");
        }
    }
}
