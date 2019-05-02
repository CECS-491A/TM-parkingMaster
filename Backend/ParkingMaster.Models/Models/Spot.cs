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
    [Table("ParkingMaster.Spots")]
    public class Spot : ISerializable
    {
        // Automatic properties
        [Key]
        public Guid SpotId { get; set; }
        public string SpotName { get; set; }
        [ForeignKey("Lot")]
        public Guid LotId { get; set; }
        public string LotName { get; set; }
        public bool IsHandicappedAccessible { get; set; }

        // If current time is after this variable, that means the parking spot is empty and available for reservation
        public DateTime ReservedUntil { get; set; } 

        // User who is currently reserving or last reserved this Spot
        [ForeignKey("User")]
        public Guid? TakenBy { get; set; }

        // Vehicle the user reserved the parking spot for
        [ForeignKey("UserVehicle")]
        public string VehicleVin { get; set; }

        // Navigation properties
        public virtual Lot Lot { get; set; }
        public virtual UserAccount User { get; set; }
        public virtual Vehicle UserVehicle { get; set; }

        // Constructors
        public Spot()
        {
            LotName = "DEFAULT";
            IsHandicappedAccessible = false;
            ReservedUntil = DateTime.Now;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SpotId", this.SpotId);
            info.AddValue("SpotName", this.SpotName);
            info.AddValue("IsHandicappedAccessible", this.IsHandicappedAccessible);
            // Check if this spot is currently available
            if(DateTime.Now.CompareTo(this.ReservedUntil) != 1)
            {
                info.AddValue("IsTaken", true);
            }
            else
            {
                info.AddValue("IsTaken", false);
            }

        }
    }
}
