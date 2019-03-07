namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PasswordSalts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ParkingMaster.UserAccount", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "ParkingMaster.UserAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        IsActive = c.Boolean(),
                        IsFirstTimeUser = c.Boolean(),
                        RoleType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthenticationTokens",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Username = c.String(),
                        ExpiresOn = c.DateTime(nullable: false),
                        Salt = c.String(),
                        TokenString = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ParkingMaster.UserAccount", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "ParkingMaster.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ParkingMaster.UserAccount", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PasswordSalts", "Id", "ParkingMaster.UserAccount");
            DropForeignKey("ParkingMaster.UserClaims", "Id", "ParkingMaster.UserAccount");
            DropForeignKey("dbo.AuthenticationTokens", "Id", "ParkingMaster.UserAccount");
            DropIndex("ParkingMaster.UserClaims", new[] { "Id" });
            DropIndex("dbo.AuthenticationTokens", new[] { "Id" });
            DropIndex("dbo.PasswordSalts", new[] { "Id" });
            DropTable("ParkingMaster.UserClaims");
            DropTable("dbo.AuthenticationTokens");
            DropTable("ParkingMaster.UserAccount");
            DropTable("dbo.PasswordSalts");
        }
    }
}
