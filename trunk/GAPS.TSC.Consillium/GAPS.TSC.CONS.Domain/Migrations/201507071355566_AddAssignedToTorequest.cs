namespace GAPS.TSC.CONS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssignedToTorequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpertRequests", "AssignedToId", c => c.Int());
            CreateIndex("dbo.ExpertRequests", "AssignedToId");
            AddForeignKey("dbo.ExpertRequests", "AssignedToId", "dbo.TeamMembers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpertRequests", "AssignedToId", "dbo.TeamMembers");
            DropIndex("dbo.ExpertRequests", new[] { "AssignedToId" });
            DropColumn("dbo.ExpertRequests", "AssignedToId");
        }
    }
}
