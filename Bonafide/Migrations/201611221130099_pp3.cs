namespace Bonafide.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pp3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudBonas", "BonafideID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudBonas", "BonafideID", c => c.String(nullable: false));
        }
    }
}
