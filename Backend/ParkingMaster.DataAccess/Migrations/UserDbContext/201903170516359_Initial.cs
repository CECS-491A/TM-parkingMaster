namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claims",
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
                .ForeignKey("ParkingMaster.UserAccount", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "ParkingMaster.UserAccount",
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
                "dbo.AuthenticationTokens",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(),
                        ExpiresOn = c.DateTime(nullable: false),
                        Salt = c.String(),
                        TokenString = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ParkingMaster.UserAccount", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "ParkingMaster.Functions",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ParkingMaster.UserClaims", "Id", "ParkingMaster.UserAccount");
            DropForeignKey("dbo.AuthenticationTokens", "Id", "ParkingMaster.UserAccount");
            DropForeignKey("dbo.Claims", "UserClaimsId", "ParkingMaster.UserClaims");
            DropIndex("dbo.AuthenticationTokens", new[] { "Id" });
            DropIndex("ParkingMaster.UserAccount", new[] { "Username" });
            DropIndex("ParkingMaster.UserClaims", new[] { "Id" });
            DropIndex("dbo.Claims", new[] { "UserClaimsId" });
            DropTable("ParkingMaster.Functions");
            DropTable("dbo.AuthenticationTokens");
            DropTable("ParkingMaster.UserAccount");
            DropTable("ParkingMaster.UserClaims");
            DropTable("dbo.Claims");
        }
    }
}
