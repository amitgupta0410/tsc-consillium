namespace GAPS.TSC.CONS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectLead_TABLE : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpecialProjectLeadMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpecialProjectLeadMaps");
        }
    }
}
