namespace Bonafide.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudBonas", "Passport", c => c.Binary());
            AddColumn("dbo.StudBonas", "Photo", c => c.Binary());
            AddColumn("dbo.StudBonas", "PassportWithStamping", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudBonas", "PassportWithStamping");
            DropColumn("dbo.StudBonas", "Photo");
            DropColumn("dbo.StudBonas", "Passport");
        }
    }
}
