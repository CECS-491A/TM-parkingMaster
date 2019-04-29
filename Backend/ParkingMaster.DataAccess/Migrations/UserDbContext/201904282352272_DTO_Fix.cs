namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DTO_Fix : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserAccountDTOes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserAccountDTOes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false),
                        SsoId = c.Guid(nullable: false),
                        IsActive = c.Boolean(),
                        IsFirstTimeUser = c.Boolean(),
                        RoleType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
