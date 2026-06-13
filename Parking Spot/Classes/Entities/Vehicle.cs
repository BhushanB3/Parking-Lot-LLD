using System.ComponentModel.DataAnnotations;

namespace Parking_Spot.Classes.Entities
{
    public class Vehicle
    {
        [Key]
        public Guid Id { get; set; }
        public required string LicensePlate { get; set; }
        public VehicleType Type { get; set; }
        public DateTime? LastParked { get; set; }
        public bool? IsParked { get; set; } = true;

    }

    public enum VehicleType
    {
        Bike,
        Car,
        Truck
    }
}
