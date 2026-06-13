using Parking_Spot.Classes;
using Parking_Spot.Classes.Entities;
using Parking_Spot.Classes.Parking;

namespace Parking_Spot.Services.IService
{
    public interface IMainService
    {
        public Task<string> AddSpot(int levelNumber, SpotType spotType);
        public Task<string> RemoveSpot(int levelNumber, Guid spotid);
        public Task<List<ParkingSpot>> GetAvailableSpots(VehicleType vehicleType);
        public Task<string> ParkVehicle(Vehicle vehicle, Guid spotId);
        public Task<string> UnParkVehicle(Guid ticketId, PaymentType payType);
    }
}
