namespace Bonafide.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudBonas", "PassportNo", c => c.String(nullable: false));
            Sql("Update dbo.StudBonas set PassportNo=BonafideID");
            DropColumn("dbo.StudBonas", "BonafideID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudBonas", "BonafideID", c => c.String());
            DropColumn("dbo.StudBonas", "PassportNo");
        }
    }
}
