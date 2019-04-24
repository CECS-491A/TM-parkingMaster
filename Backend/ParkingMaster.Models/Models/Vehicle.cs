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
    [Table("ParkingMaster.Vehicle")]
    public class Vehicle : ISerializable
    {
        // Automatic Properties
        [ForeignKey("UserAccount")]
        public Guid OwnerId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string State { get; set; }
        public string Plate { get; set; }
        [Key]
        public string Vin { get; set; }

        // Navigational Property
        public virtual UserAccount UserAccount { get; set; }

        // Constructors
        public Vehicle() { }

        public Vehicle(string make, string model, int year, string state, string plate, string vin)
        {
            Make = make;
            Model = model;
            Year = year;
            State = state;
            Plate = plate;
            Vin = vin;
        }

        public Vehicle(Guid id, string make, string model, int year, string state, string plate, string vin)
        {
            OwnerId = id;
            Make = make;
            Model = model;
            Year = year;
            State = state;
            Plate = plate;
            Vin = vin;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Vin", this.Vin);
            info.AddValue("Plate", this.Plate);
        }
    }
}
