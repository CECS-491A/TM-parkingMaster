using ParkingMaster.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Context
{
	public class AuthenticationContext : DbContext
	{
		public DbSet<UserAccount> UserAccounts { get; set; }
		public DbSet<PasswordSalt> PasswordSalts { get; set; }
		public DbSet<AuthenticationToken> AuthenticationTokens { get; set; }
		public DbSet<SecurityQuestions>SecurityQuestions { get; set; }


		public AuthenticationContext() : base("ParkingMaster") { }
	}

}
