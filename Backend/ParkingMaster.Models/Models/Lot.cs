using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Models
{
    public class Lot
    {
        // Automatic properties
        [Key]
        public Guid LotId { get; set; }
        //[ForeignKey("UserAccount")]
        //public Guid OwnerId { get; set; }
        public string LotName { get; set; }

        // Navigation Properties
        //public virtual UserAccount UserAccount { get; set; }
        public ICollection<Spot> Spots { get; set; }

        // Constructors
        public Lot()
        {
            this.Spots = new List<Spot>();
        }

    }
}
