namespace GAPS.TSC.CONS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpertRequest_BdLeadName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpertRequests", "BdLeadName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpertRequests", "BdLeadName");
        }
    }
}
