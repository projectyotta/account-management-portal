namespace CMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexDataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 320),
                        PhoneNumber = c.Long(nullable: false),
                        OfficeNumber = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        JobID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.JobID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.ContractEmployee",
                c => new
                    {
                        ContractID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContractID, t.EmployeeID })
                .ForeignKey("dbo.Contract", t => t.ContractID, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.ContractID)
                .Index(t => t.EmployeeID);


            // Create  a department for course to point to.
            Sql("INSERT INTO dbo.Job (Name) VALUES ('Temp')");
            //  default value for FK points to department created above.
            AddColumn("dbo.Contract", "JobID", c => c.Int(nullable: false, defaultValue: 1));
            //AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false));
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contract", "JobID", "dbo.Job");
            DropForeignKey("dbo.Job", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.ContractEmployee", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.ContractEmployee", "ContractID", "dbo.Contract");
            DropIndex("dbo.ContractEmployee", new[] { "EmployeeID" });
            DropIndex("dbo.ContractEmployee", new[] { "ContractID" });
            DropIndex("dbo.Job", new[] { "EmployeeID" });
            DropIndex("dbo.Contract", new[] { "JobID" });
            DropColumn("dbo.Contract", "JobID");
            DropTable("dbo.ContractEmployee");
            DropTable("dbo.Job");
            DropTable("dbo.Employee");
        }
    }
}
