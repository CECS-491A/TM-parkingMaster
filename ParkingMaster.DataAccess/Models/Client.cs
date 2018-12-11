using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ParkingMaster.DataAccess.Models
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Client()
        {
            Id = Guid.NewGuid();
            Name = null;
        }

    }
}
