namespace CMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
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
                      
                      SELECT t0.[JobID]
                      FROM [dbo].[Job] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[JobID] = @JobID"
            );
            
            CreateStoredProcedure(
                "dbo.Job_Update",
                p => new
                    {
                        JobID = p.Int(),
                        Name = p.String(maxLength: 50),
                        EmployeeID = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Job]
                      SET [Name] = @Name, [EmployeeID] = @EmployeeID
                      WHERE ([JobID] = @JobID)"
            );
            
            CreateStoredProcedure(
                "dbo.Job_Delete",
                p => new
                    {
                        JobID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Job]
                      WHERE ([JobID] = @JobID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Job_Delete");
            DropStoredProcedure("dbo.Job_Update");
            DropStoredProcedure("dbo.Job_Insert");
        }
    }
}
