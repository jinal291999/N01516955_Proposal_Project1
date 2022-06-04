namespace N01516955_Proposal_Project1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAppointmentKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Appointments");
            AddColumn("dbo.Appointments", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Appointments", "FirstName", c => c.String());
            AddPrimaryKey("dbo.Appointments", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Appointments");
            AlterColumn("dbo.Appointments", "FirstName", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Appointments", "Id");
            AddPrimaryKey("dbo.Appointments", "FirstName");
        }
    }
}
