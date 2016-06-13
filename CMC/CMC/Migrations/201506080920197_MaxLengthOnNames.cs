namespace CMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthOnNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Project", "WON", c => c.String());
            AlterColumn("dbo.Project", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Project", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Project", "WON", c => c.String(maxLength: 50));
        }
    }
}
