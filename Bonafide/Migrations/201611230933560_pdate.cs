namespace Bonafide.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudBonas", "InitDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudBonas", "InitDate", c => c.String());
        }
    }
}
