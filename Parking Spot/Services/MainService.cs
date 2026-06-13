using Microsoft.EntityFrameworkCore;
using Parking_Spot.Classes;
using Parking_Spot.Classes.Entities;
using Parking_Spot.Classes.Parking;
using Parking_Spot.Data;
using Parking_Spot.Services.IService;
using System.Threading.Tasks;

namespace Parking_Spot.Services
{
    public class MainService : IMainService
    {
        private readonly ApplicationDBContext _db;

        public MainService(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<string> AddSpot(int levelNumber, SpotType spotType)
        {
            int count = _db.ParkingSpots.Count(c => c.LevelNumber == levelNumber);
            if (count < 10)
            {
                var toAdd = new ParkingSpot
                {
                    LevelNumber = levelNumber,
                    SpotType = spotType,
                };
                _db.ParkingSpots.Add(toAdd);

                if (await _db.SaveChangesAsync() > 0)
                {
                    return "Spot added successfully";
                }
                return "Error saving changes to the database";
            }
            else if (count >= 10)
            {
                return "Cannot add more than 10 spots on a level";
            }
            else
            {
                return "Error adding spot";
            }
        }

        public async Task<string> RemoveSpot(int levelNumber, Guid SpotId)
        {
            var spotToRemove = _db.ParkingSpots.FirstOrDefault(s => s.LevelNumber == levelNumber
                                                                && s.Id == SpotId
                                                                && !s.IsOccupied
                                                                && s.IsActive);
            if (spotToRemove != null)
            {
                spotToRemove.IsActive = false;
                if (await _db.SaveChangesAsync() > 0)
                {
                    return "Spot removed successfully";
                }
                return "Error saving changes to the database";
            }
            else
            {
                return "Either spot is occupied or on another level";
            }
        }

        public async Task<List<ParkingSpot>> GetAvailableSpots(VehicleType vehicleType)
        {
            var availableSpots = await _db.ParkingSpots.Where(c => !c.IsOccupied && c.IsActive).ToListAsync();
            List<ParkingSpot> availableFitSpots = new List<ParkingSpot>();
            foreach (var spot in availableSpots)
            {
                bool canFit = ParkingSpotService.CanFitVehicle(spot, vehicleType);
                if (canFit)
                {
                    availableFitSpots.Add(spot);
                }
            }
            return availableFitSpots;
        }

        public async Task<string> ParkVehicle(Vehicle vehicle, Guid spotId)
        {
            var intendedSpot = await _db.ParkingSpots.FirstOrDefaultAsync(s => s.Id == spotId);
            if (intendedSpot != null)
            {
                if (ParkingSpotService.CanFitVehicle(intendedSpot, vehicle.Type) && !intendedSpot.IsOccupied && intendedSpot.IsActive)
                {
                    var parkedVehicle = await _db.Vehicles.FirstOrDefaultAsync(v => v.LicensePlate == vehicle.LicensePlate);

                    intendedSpot.IsOccupied = true;
                    
                    var ticket = new Ticket
                    {
                        EntryTime = DateTime.Now,
                        Vehicle = parkedVehicle == null ? vehicle : parkedVehicle,
                        ParkingSpot = intendedSpot
                    };

                    if (parkedVehicle != null)
                    {
                        parkedVehicle.LastParked = ticket.EntryTime;
                        intendedSpot.Vehicle = parkedVehicle;
                    }
                    else
                    {
                        vehicle.LastParked = ticket.EntryTime;
                        intendedSpot.Vehicle = vehicle;
                        _db.Vehicles.Add(vehicle);
                    }

                    _db.Tickets.Add(ticket);

                    if (await _db.SaveChangesAsync() > 0)
                    {
                        return "Vehicle parked successfully";
                    }
                    return "Error saving changes to the database";
                }
                else
                {
                    return "Vehicle cannot fit in the spot or spot is occupied/inactive";
                }
            }
            else
            {
                return "Spot not found";
            }
        }

        public async Task<string> UnParkVehicle(Guid ticketId, PaymentType payType)
        {
            var parkingdetails = await (from t in _db.Tickets
                                 where t.Id == ticketId
                                    && t.ExitTime == null
                                 join spot in _db.ParkingSpots on t.ParkingSpot.Id equals spot.Id
                                 join v in _db.Vehicles on t.Vehicle.Id equals v.Id
                                 select new
                                 {
                                     Ticket = t,
                                     Spot = spot,
                                     Vehicle = v
                                 }).FirstOrDefaultAsync();

            if(parkingdetails != null)
            {
                var Parkfees = TicketService.CalculateTotalFees(parkingdetails.Ticket);
                var paid = TicketService.MakePayment(parkingdetails.Ticket, payType, Parkfees);
                if (paid)
                {
                    parkingdetails.Ticket.ExitTime = DateTime.Now;
                    parkingdetails.Ticket.TotalFees = Parkfees;
                    parkingdetails.Ticket.IsPaid = true;
                    parkingdetails.Ticket.PaymentType = payType;

                    parkingdetails.Vehicle.IsParked = false;
                    parkingdetails.Spot.IsOccupied = false;
                    parkingdetails.Spot.Vehicle = null;
                }
                else
                {
                    return "Payment failed. Please try again.";
                }
            }
            else
            {
                return "Active parking ticket not found for the provided ID. Please provide latest ticket";
            }

            if(await _db.SaveChangesAsync() > 0)
            {
                return "Vehicle Unparked successfully";
            }

            return "Error saving changes to the database";
        }
    }
}
