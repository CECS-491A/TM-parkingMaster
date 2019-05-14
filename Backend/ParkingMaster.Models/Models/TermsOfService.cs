using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingMaster.Models.Models
{
    [Table("ParkingMaster.TOS")]
    public class TermsOfService
    {
        [Key]
        public Guid Id { get; set; }
        public string TosName { get; set; }
        public bool IsActive { get; set; }
        public string Content { get; set; }

        public TermsOfService()
        {
            Id = new Guid();
            TosName = "Default";
            IsActive = false;
            Content = "This is just the default TOS.";
        }
    }
}
