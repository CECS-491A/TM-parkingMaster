using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ParkingMaster.Models.Models
{
   
    // Defines properties of a vehicle registered to a user account
 
    [Table("ParkingMaster.Vehicle")]
    public class Vehicle 
    {
        // Automatic Properties
        [ForeignKey("UserAccount")]
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string State { get; set;}
        public string Plate { get; set;}
        public string Vin { get; set;}

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
            Id = id;
            Make = make;
            Model = model;
            Year = year;
            State = state;
            Plate = plate;
            Vin = vin;
        }
    }
