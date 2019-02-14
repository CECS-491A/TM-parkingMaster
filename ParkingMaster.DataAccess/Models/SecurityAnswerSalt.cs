using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingMaster.DataAccess.Models
{
   
    [Table("ParkingMaster.SecurityAnswerSalt")]
    public class SecurityAnswerSalt : Salt, Entity
    {
        [Key]
        [ForeignKey("SecurityQuestion")]
        public int? Id { get; set; }
        public string Salt { get; set; }

        public virtual SecurityQuestion SecurityQuestion { get; set; }
    }
}
