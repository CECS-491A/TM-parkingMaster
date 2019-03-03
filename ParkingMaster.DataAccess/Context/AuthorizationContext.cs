using ParkingMaster.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Context
{
	public class AuthorizationContext : DbContext
	{
		public DbSet<UserClaims> Claims { get; set; }
		public AuthorizationContext() : base("ParkingMaster") { }
	}
}
