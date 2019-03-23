using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingMaster.Models.Models
{

    [Table("ParkingMaster.Functions")]
    public class Function
    {
        [Key]
        public string Name { get; set; }
        public Boolean Active { get; set; }

        public Function()
        {

        }

        public Function(string n, Boolean a)
        {
            Name = n;
            Active = a;
        }
    }
}
