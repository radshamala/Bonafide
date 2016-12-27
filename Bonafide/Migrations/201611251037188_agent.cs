namespace Bonafide.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudBonas", "Agent", c => c.String());
            AlterColumn("dbo.StudBonas", "AgentPhone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudBonas", "AgentPhone", c => c.String(nullable: false));
            AlterColumn("dbo.StudBonas", "Agent", c => c.String(nullable: false));
        }
    }
}
