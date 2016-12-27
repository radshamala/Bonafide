namespace Bonafide.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class del : DbMigration
    {
        public override void Up()
        {
            Sql("Delete from  StudBonas where  passportno='Sim123'");
        }
        
        public override void Down()
        {
        }
    }
}
