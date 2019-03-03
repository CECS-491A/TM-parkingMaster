using ParkingMaster.Models.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess
{
	public class UserContext : DbContext
	{
		public DbSet<UserAccount> UserAccounts { get; set; }
		public DbSet<SecurityQuestions> SecurityQuestions { get; set; }
		public DbSet<UserClaims> UserClaims { get; set; }
		public DbSet<SecurityAnswerSalt> SecurityAnswerSalts { get; set; }
		public DbSet<PasswordSalt> PasswordSalts { get; set; }
		public UserContext() : base("ParkingMaster") { }

	}
}
