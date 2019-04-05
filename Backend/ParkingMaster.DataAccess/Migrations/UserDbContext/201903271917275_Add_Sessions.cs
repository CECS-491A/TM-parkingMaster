namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Sessions : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "ParkingMaster.UserAccount", newName: "UserAccounts");
            MoveTable(name: "dbo.Claims", newSchema: "ParkingMaster");
            DropForeignKey("dbo.AuthenticationTokens", "Id", "ParkingMaster.UserAccount");
            DropIndex("dbo.AuthenticationTokens", new[] { "Id" });
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
            
            DropTable("dbo.AuthenticationTokens");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("ParkingMaster.Sessions", "Id", "ParkingMaster.UserAccounts");
            DropIndex("ParkingMaster.Sessions", new[] { "Id" });
            DropTable("ParkingMaster.Sessions");
            CreateIndex("dbo.AuthenticationTokens", "Id");
            AddForeignKey("dbo.AuthenticationTokens", "Id", "ParkingMaster.UserAccount", "Id");
            MoveTable(name: "ParkingMaster.Claims", newSchema: "dbo");
            RenameTable(name: "ParkingMaster.UserAccounts", newName: "UserAccount");
        }
    }
}
