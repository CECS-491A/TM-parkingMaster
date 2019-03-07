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
        public AuthorizationContext()
        {
            this.Database.Connection.ConnectionString = "Data Source=localhost;Initial Catalog=ParkingMaster;Integrated Security=True";
        }

        public DbSet<UserClaims> Claims { get; set; }
	}
}
