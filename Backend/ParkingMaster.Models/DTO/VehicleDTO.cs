using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{

    /// DTO properties of Vehicle

    public class VehicleDTO
    {
        // Automatic Properties
        public Guid ID { get; set;}
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string State { get; set;}
        public string Plate { get; set;}
        public string Vin { get; set;}

        // Constructors
        public VehicleDTO() { }

        public VehicleDTO(string make, string model, int year, string state, string plate, string vi)
        {
            Make = make;
            Model = model;
            Year = year;
            State = state;
            Plate = plate;
            Vin = vin;
        }

        public VehicleDTO(VehicleDto vehicle)
        {

            Account = vehicle.ID;
            Make = vehicle.Make;
            Model = vehicle.Model;
            Year = vehicle.Year;
            State = vehicle.State;
            Plate = vehicle.Plate;
            Vin = vehicle.Vin;
        }
    }
}
