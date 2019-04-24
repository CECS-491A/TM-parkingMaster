using ParkingMaster.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ParkingMaster.Models.Models
{
    [Serializable]
    [Table("ParkingMaster.Lots")]
    public class Lot : ISerializable
    {
        // Automatic properties
        [Key]
        public Guid LotId { get; set; }
        [ForeignKey("UserAccount")]
        public Guid OwnerId { get; set; }
        public string LotName { get; set; }
        public string Address { get; set; }
        public double Cost { get; set; }

        // Navigation Properties
        public virtual UserAccountDTO UserAccount { get; set; } // change to UserAccount
        public List<Spot> Spots { get; set; }

        //Constructors
        public Lot()
        {
            this.Spots = new List<Spot>();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LotId", this.LotId);
            info.AddValue("LotName", this.LotName);
            info.AddValue("Address", this.Address);
            info.AddValue("Cost", this.Cost);
        }
    }
}
