namespace Bonafide.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pp2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudBonas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Duration = c.Int(nullable: false),
                        Course = c.String(nullable: false),
                        InitDate = c.String(),
                        Agent = c.String(nullable: false),
                        AgentPhone = c.String(nullable: false),
                        Note = c.String(nullable: false, maxLength: 1000),
                        Fees = c.Int(nullable: false),
                        FeesPaid = c.Int(nullable: false),
                        Commission = c.Int(nullable: false),
                        CommissionPaid = c.Int(nullable: false),
                        HasArrived = c.Boolean(nullable: false),
                        BonafideID = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StudBonas");
        }
    }
}
