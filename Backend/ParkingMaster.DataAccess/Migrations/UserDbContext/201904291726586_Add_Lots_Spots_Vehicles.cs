namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Lots_Spots_Vehicles : DbMigration
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
                        MapFilePath = c.String(),
                    })
                .PrimaryKey(t => t.LotId)
                .ForeignKey("ParkingMaster.UserAccounts", t => t.OwnerId, cascadeDelete: true)
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
                        ReservedUntil = c.DateTime(nullable: false),
                        TakenBy = c.Guid(),
                        VehicleVin = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SpotId)
                .ForeignKey("ParkingMaster.Lots", t => t.LotId, cascadeDelete: true)
                .ForeignKey("ParkingMaster.UserAccounts", t => t.TakenBy)
                .ForeignKey("ParkingMaster.Vehicle", t => t.VehicleVin)
                .Index(t => t.LotId)
                .Index(t => t.TakenBy)
                .Index(t => t.VehicleVin);
            
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
            DropForeignKey("ParkingMaster.Lots", "OwnerId", "ParkingMaster.UserAccounts");
            DropForeignKey("ParkingMaster.Spots", "VehicleVin", "ParkingMaster.Vehicle");
            DropForeignKey("ParkingMaster.Vehicle", "OwnerId", "ParkingMaster.UserAccounts");
            DropForeignKey("ParkingMaster.Spots", "TakenBy", "ParkingMaster.UserAccounts");
            DropForeignKey("ParkingMaster.Spots", "LotId", "ParkingMaster.Lots");
            DropIndex("ParkingMaster.Vehicle", new[] { "OwnerId" });
            DropIndex("ParkingMaster.Spots", new[] { "VehicleVin" });
            DropIndex("ParkingMaster.Spots", new[] { "TakenBy" });
            DropIndex("ParkingMaster.Spots", new[] { "LotId" });
            DropIndex("ParkingMaster.Lots", new[] { "OwnerId" });
            DropTable("ParkingMaster.Vehicle");
            DropTable("ParkingMaster.Spots");
            DropTable("ParkingMaster.Lots");
        }
    }
}
