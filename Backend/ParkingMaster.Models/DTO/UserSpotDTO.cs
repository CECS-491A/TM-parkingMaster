using System;
using System.Runtime.Serialization;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Models.DTO
{
    [Serializable]
    public class UserSpotDTO : ISerializable
    {
        // Automatic properties
        public Guid SpotId { get; set; }
        public string SpotName { get; set; }
        public Guid LotId { get; set; }
        public string LotAddress { get; set; }
        public string LotName { get; set; }
        public bool IsHandicappedAccessible { get; set; }

        // If current time is after this variable, that means the parking spot is empty and available for reservation
        public DateTime ReservedUntil { get; set; }

        // Vehicle the user reserved the parking spot for
        public string VehicleVin { get; set; }
        public string VehiclePlate { get; set; }

        // Constructors
        public UserSpotDTO(Spot spot)
        {
            SpotId = spot.SpotId;
            SpotName = spot.SpotName;
            LotId = spot.LotId;
            LotName = spot.LotName;
            IsHandicappedAccessible = spot.IsHandicappedAccessible;
            ReservedUntil = spot.ReservedUntil;
            VehicleVin = spot.VehicleVin;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SpotId", this.SpotId);
            info.AddValue("SpotName", this.SpotName);
            info.AddValue("LotId", this.LotId);
            info.AddValue("LotName", this.LotName);
            info.AddValue("LotAddress", this.LotAddress);
            info.AddValue("IsHandicappedAccessible", this.IsHandicappedAccessible);

            // Determine time left on reservation so frontend can calculate end time
            TimeSpan timeLeft = this.ReservedUntil.Subtract(DateTime.Now);

            info.AddValue("SecondsLeft", this.ReservedUntil.Subtract(DateTime.Now).TotalSeconds);
            info.AddValue("VehicleVin", this.VehicleVin);
            info.AddValue("VehiclePlate", this.VehiclePlate);

        }
    }
}