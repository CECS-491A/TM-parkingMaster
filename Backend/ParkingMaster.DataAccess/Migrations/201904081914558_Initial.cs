namespace ParkingMaster.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ParkingMaster.Claims",
                c => new
                    {
                        ClaimId = c.Guid(nullable: false),
                        UserClaimsId = c.Guid(nullable: false),
                        Title = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ClaimId)
                .ForeignKey("ParkingMaster.UserClaims", t => t.UserClaimsId, cascadeDelete: true)
                .Index(t => t.UserClaimsId);
            
            CreateTable(
                "ParkingMaster.UserClaims",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ParkingMaster.UserAccounts", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "ParkingMaster.UserAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SsoId = c.Guid(nullable: false),
                        Username = c.String(maxLength: 350),
                        IsActive = c.Boolean(),
                        IsFirstTimeUser = c.Boolean(),
                        RoleType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true);
            
            CreateTable(
                "ParkingMaster.Functions",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
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
                "ParkingMaster.Sessions",
                c => new
                    {
                        SessionId = c.Guid(nullable: false),
                        Id = c.Guid(nullable: false),
                        ExpiringAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SessionId)
                .ForeignKey("ParkingMaster.UserAccounts", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ParkingMaster.Sessions", "Id", "ParkingMaster.UserAccounts");
            DropForeignKey("dbo.Lots", "OwnerId", "dbo.UserAccountDTOes");
            DropForeignKey("dbo.Spots", "LotId", "dbo.Lots");
            DropForeignKey("ParkingMaster.UserClaims", "Id", "ParkingMaster.UserAccounts");
            DropForeignKey("ParkingMaster.Claims", "UserClaimsId", "ParkingMaster.UserClaims");
            DropIndex("ParkingMaster.Sessions", new[] { "Id" });
            DropIndex("dbo.Spots", new[] { "LotId" });
            DropIndex("dbo.Lots", new[] { "OwnerId" });
            DropIndex("ParkingMaster.UserAccounts", new[] { "Username" });
            DropIndex("ParkingMaster.UserClaims", new[] { "Id" });
            DropIndex("ParkingMaster.Claims", new[] { "UserClaimsId" });
            DropTable("ParkingMaster.Sessions");
            DropTable("dbo.UserAccountDTOes");
            DropTable("dbo.Spots");
            DropTable("dbo.Lots");
            DropTable("ParkingMaster.Functions");
            DropTable("ParkingMaster.UserAccounts");
            DropTable("ParkingMaster.UserClaims");
            DropTable("ParkingMaster.Claims");
        }
    }
}
