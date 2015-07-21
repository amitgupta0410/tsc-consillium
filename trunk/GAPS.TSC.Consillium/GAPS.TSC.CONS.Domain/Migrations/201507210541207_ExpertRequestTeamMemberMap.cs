namespace GAPS.TSC.CONS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpertRequestTeamMemberMap : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExpertRequests", "AssignedToId", "dbo.TeamMembers");
            DropIndex("dbo.ExpertRequests", new[] { "AssignedToId" });
            CreateTable(
                "dbo.ExpertRequestTeamMemberMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignedToId = c.Int(nullable: false),
                        ExpertRequestId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpertRequests", t => t.ExpertRequestId, cascadeDelete: true)
                .ForeignKey("dbo.TeamMembers", t => t.AssignedToId, cascadeDelete: true)
                .Index(t => t.AssignedToId)
                .Index(t => t.ExpertRequestId);
            
            DropColumn("dbo.ExpertRequests", "AssignedToId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExpertRequests", "AssignedToId", c => c.Int());
            DropForeignKey("dbo.ExpertRequestTeamMemberMaps", "AssignedToId", "dbo.TeamMembers");
            DropForeignKey("dbo.ExpertRequestTeamMemberMaps", "ExpertRequestId", "dbo.ExpertRequests");
            DropIndex("dbo.ExpertRequestTeamMemberMaps", new[] { "ExpertRequestId" });
            DropIndex("dbo.ExpertRequestTeamMemberMaps", new[] { "AssignedToId" });
            DropTable("dbo.ExpertRequestTeamMemberMaps");
            CreateIndex("dbo.ExpertRequests", "AssignedToId");
            AddForeignKey("dbo.ExpertRequests", "AssignedToId", "dbo.TeamMembers", "Id");
        }
    }
}
