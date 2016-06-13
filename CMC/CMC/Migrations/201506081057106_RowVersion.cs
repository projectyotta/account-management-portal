namespace CMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RowVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Job", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AlterStoredProcedure(
                "dbo.Job_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 50),
                        EmployeeID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Job]([Name], [EmployeeID])
                      VALUES (@Name, @EmployeeID)
                      
                      DECLARE @JobID int
                      SELECT @JobID = [JobID]
                      FROM [dbo].[Job]
                      WHERE @@ROWCOUNT > 0 AND [JobID] = scope_identity()
                      
                      SELECT t0.[JobID], t0.[RowVersion]
                      FROM [dbo].[Job] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[JobID] = @JobID"
            );
            
            AlterStoredProcedure(
                "dbo.Job_Update",
                p => new
                    {
                        JobID = p.Int(),
                        Name = p.String(maxLength: 50),
                        EmployeeID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"UPDATE [dbo].[Job]
                      SET [Name] = @Name, [EmployeeID] = @EmployeeID
                      WHERE (([JobID] = @JobID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Job] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[JobID] = @JobID"
            );
            
            AlterStoredProcedure(
                "dbo.Job_Delete",
                p => new
                    {
                        JobID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"DELETE [dbo].[Job]
                      WHERE (([JobID] = @JobID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.Job", "RowVersion");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
