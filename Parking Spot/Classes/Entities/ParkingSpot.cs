using Parking_Spot.Classes.Parking;
using System.ComponentModel.DataAnnotations;

namespace Parking_Spot.Classes.Entities
{
    public class ParkingSpot
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsOccupied { get; set; } = false;
        public int LevelNumber { get; set; }
        public SpotType SpotType { get; set; }
        public Vehicle? Vehicle { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
