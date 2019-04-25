namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccountDTOes", "SsoId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAccountDTOes", "SsoId");
        }
    }
}
