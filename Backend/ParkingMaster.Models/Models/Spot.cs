using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Models
{
    public class Spot
    {
        // Automatic properties
        [Key]
        public Guid SpotId { get; set; }
        public string SpotName { get; set; }
        [ForeignKey("Lot")]
        public Guid LotId { get; set; }
        public string LotName { get; set; }
        public bool IsHandicappedAccessible { get; set; }
        public bool IsTaken { get; set; } // remove ? --> DateTime IsFreeAt --> datetimenow - something
        // public Guid IsTakenBy --> FK UserID, null when added

        // Navigation properties
        public virtual Lot Lot { get; set; }
        // nav prop for userID table ...

        // Constructors
        public Spot()
        {
            LotName = "DEFAULT";
            IsHandicappedAccessible = false;
            IsTaken = false;
        }
    }
}
