namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Vehicles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lots",
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
                "dbo.Spots",
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
                .ForeignKey("dbo.Lots", t => t.LotId, cascadeDelete: true)
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
            
            CreateTable(
                "ParkingMaster.Vehicle",
                c => new
                    {
                        Vin = c.String(nullable: false, maxLength: 128),
                        OwnerId = c.Guid(nullable: false),
                        Make = c.String(),
                        Model = c.String(),
                        Year = c.Int(nullable: false),
                        State = c.String(),
                        Plate = c.String(),
                    })
                .PrimaryKey(t => t.Vin)
                .ForeignKey("ParkingMaster.UserAccounts", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ParkingMaster.Vehicle", "OwnerId", "ParkingMaster.UserAccounts");
            DropForeignKey("dbo.Lots", "OwnerId", "dbo.UserAccountDTOes");
            DropForeignKey("dbo.Spots", "LotId", "dbo.Lots");
            DropIndex("ParkingMaster.Vehicle", new[] { "OwnerId" });
            DropIndex("dbo.Spots", new[] { "LotId" });
            DropIndex("dbo.Lots", new[] { "OwnerId" });
            DropTable("ParkingMaster.Vehicle");
            DropTable("dbo.UserAccountDTOes");
            DropTable("dbo.Spots");
            DropTable("dbo.Lots");
        }
    }
}
