using Parking_Spot.Classes.Entities;

namespace Parking_Spot.Classes.Parking
{
    public class ParkingSpotService
    {
        public static bool CanFitVehicle(ParkingSpot spot, VehicleType vehicle)
        {
            if (spot.IsOccupied) return false;
            switch (spot.SpotType)
            {
                case SpotType.Small:
                    return vehicle == VehicleType.Bike;
                case SpotType.Mid:
                    return vehicle == VehicleType.Bike ||
                            vehicle == VehicleType.Car;
                case SpotType.Large:
                    return vehicle == VehicleType.Bike ||
                            vehicle == VehicleType.Car ||
                            vehicle == VehicleType.Truck;
                default:
                    return false;
            }
        }

    }
    public enum SpotType
    {
        Small,
        Mid,
        Large
    }
}
