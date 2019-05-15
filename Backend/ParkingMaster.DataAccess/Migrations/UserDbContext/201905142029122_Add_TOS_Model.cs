namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TOS_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ParkingMaster.TOS",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TosName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("ParkingMaster.TOS");
        }
    }
}
