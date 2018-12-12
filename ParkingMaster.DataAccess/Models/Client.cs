using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.DataAccess.Models
{
    public class Client
    {
        
        public Guid Id { get; set; }
        [Key]
        public string Email { get; set; }
        public ICollection<Claim> ClientClaims { get; set; }

        public Client()
        {
            Id = Guid.NewGuid();
            Email = null;
            ClientClaims = new List<Claim>();
        }

    }
}
