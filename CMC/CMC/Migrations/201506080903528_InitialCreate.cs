namespace CMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Allocation",
                c => new
                    {
                        AllocationID = c.Int(nullable: false, identity: true),
                        ContractID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AllocationID)
                .ForeignKey("dbo.Contract", t => t.ContractID, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ContractID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Contract",
                c => new
                    {
                        ContractID = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        value = c.Int(nullable: false),
                        currency = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContractID);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WON = c.String(maxLength: 50),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Allocation", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.Allocation", "ContractID", "dbo.Contract");
            DropIndex("dbo.Allocation", new[] { "ProjectID" });
            DropIndex("dbo.Allocation", new[] { "ContractID" });
            DropTable("dbo.Project");
            DropTable("dbo.Contract");
            DropTable("dbo.Allocation");
        }
    }
}
