namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Lots : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ParkingMaster.Lots",
                c => new
                    {
                        LotId = c.Guid(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        LotName = c.String(),
                        Address = c.String(),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.LotId)
                .ForeignKey("dbo.UserAccountDTOes", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "ParkingMaster.Spots",
                c => new
                    {
                        SpotId = c.Guid(nullable: false),
                        SpotName = c.String(),
                        LotId = c.Guid(nullable: false),
                        LotName = c.String(),
                        IsHandicappedAccessible = c.Boolean(nullable: false),
                        IsTaken = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SpotId)
                .ForeignKey("ParkingMaster.Lots", t => t.LotId, cascadeDelete: true)
                .Index(t => t.LotId);
            
            CreateTable(
                "dbo.UserAccountDTOes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false),
                        IsActive = c.Boolean(),
                        IsFirstTimeUser = c.Boolean(),
                        RoleType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ParkingMaster.Lots", "OwnerId", "dbo.UserAccountDTOes");
            DropForeignKey("ParkingMaster.Spots", "LotId", "ParkingMaster.Lots");
            DropIndex("ParkingMaster.Spots", new[] { "LotId" });
            DropIndex("ParkingMaster.Lots", new[] { "OwnerId" });
            DropTable("dbo.UserAccountDTOes");
            DropTable("ParkingMaster.Spots");
            DropTable("ParkingMaster.Lots");
        }
    }
}
