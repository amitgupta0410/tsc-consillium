namespace GAPS.TSC.CONS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpertRequestsScoping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attachments", "ExpertRequest_Id", "dbo.ExpertRequests");
            DropIndex("dbo.Attachments", new[] { "ExpertRequest_Id" });
           // RenameColumn(table: "dbo.ExpertRequestScopingDocumentMaps", name: "ExpertRequest_Id", newName: "ExpertRequestId");
            CreateTable(
                "dbo.ExpertRequestScopingDocumentMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpertRequestId = c.Int(nullable: false),
                        AttachmentId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attachments", t => t.AttachmentId, cascadeDelete: true)
                .ForeignKey("dbo.ExpertRequests", t => t.ExpertRequestId, cascadeDelete: true)
                .Index(t => t.ExpertRequestId)
                .Index(t => t.AttachmentId);
            
            AddColumn("dbo.ExpertRequests", "ApprovalDocumentId", c => c.Int(nullable: false));
            DropColumn("dbo.Attachments", "ExpertRequest_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attachments", "ExpertRequest_Id", c => c.Int());
            DropForeignKey("dbo.ExpertRequestScopingDocumentMaps", "ExpertRequestId", "dbo.ExpertRequests");
            DropForeignKey("dbo.ExpertRequestScopingDocumentMaps", "AttachmentId", "dbo.Attachments");
            DropIndex("dbo.ExpertRequestScopingDocumentMaps", new[] { "AttachmentId" });
            DropIndex("dbo.ExpertRequestScopingDocumentMaps", new[] { "ExpertRequestId" });
            DropColumn("dbo.ExpertRequests", "ApprovalDocumentId");
            DropTable("dbo.ExpertRequestScopingDocumentMaps");
            RenameColumn(table: "dbo.ExpertRequestScopingDocumentMaps", name: "ExpertRequestId", newName: "ExpertRequest_Id");
            CreateIndex("dbo.Attachments", "ExpertRequest_Id");
            AddForeignKey("dbo.Attachments", "ExpertRequest_Id", "dbo.ExpertRequests", "Id");
        }
    }
}
