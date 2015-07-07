namespace GAPS.TSC.CONS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pending : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Calls", "ExpertRequestId", c => c.Int(nullable: false));
            CreateIndex("dbo.Calls", "ExpertRequestId");
            AddForeignKey("dbo.Calls", "ExpertRequestId", "dbo.ExpertRequests", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Calls", "ExpertRequestId", "dbo.ExpertRequests");
            DropIndex("dbo.Calls", new[] { "ExpertRequestId" });
            DropColumn("dbo.Calls", "ExpertRequestId");
        }
    }
}
