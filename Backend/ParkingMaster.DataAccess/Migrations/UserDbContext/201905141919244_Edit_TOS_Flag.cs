namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_TOS_Flag : DbMigration
    {
        public override void Up()
        {
            AddColumn("ParkingMaster.UserAccounts", "AcceptedTOS", c => c.Boolean(nullable: false));
            AlterColumn("ParkingMaster.UserAccounts", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("ParkingMaster.UserAccounts", "IsFirstTimeUser");
        }
        
        public override void Down()
        {
            AddColumn("ParkingMaster.UserAccounts", "IsFirstTimeUser", c => c.Boolean());
            AlterColumn("ParkingMaster.UserAccounts", "IsActive", c => c.Boolean());
            DropColumn("ParkingMaster.UserAccounts", "AcceptedTOS");
        }
    }
}
