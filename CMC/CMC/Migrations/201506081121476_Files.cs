namespace CMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.File",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        File_FileId = c.Int(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.File", t => t.File_FileId)
                .Index(t => t.EmployeeID)
                .Index(t => t.File_FileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "File_FileId", "dbo.File");
            DropForeignKey("dbo.File", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.File", new[] { "File_FileId" });
            DropIndex("dbo.File", new[] { "EmployeeID" });
            DropTable("dbo.File");
        }
    }
}
