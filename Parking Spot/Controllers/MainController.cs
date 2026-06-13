using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking_Spot.Classes;
using Parking_Spot.Classes.Entities;
using Parking_Spot.Classes.Parking;
using Parking_Spot.Services;
using Parking_Spot.Services.IService;

namespace Parking_Spot.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IMainService _mainService;

        public MainController(IMainService mainService)
        {
            _mainService = mainService;
        }

        [HttpPost]
        [Route("AddSpot")]
        public async Task<ActionResult> AddSpot(int levelNumber, SpotType spotType)
        {
            var result = await _mainService.AddSpot(levelNumber, spotType);
            return Ok(result);
        }

        [HttpPost]
        [Route("RemoveSpot")]
        public async Task<ActionResult> RemoveSpot(int levelNumber, Guid spotId)
        {
            var result = await _mainService.RemoveSpot(levelNumber, spotId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetSpots")]
        public async Task<ActionResult> GetSpots(VehicleType vehicleType)
        {
            List<ParkingSpot> spots = await _mainService.GetAvailableSpots(vehicleType);
            return Ok(spots);
        }

        [HttpPost]
        [Route("ParkVehicle")]
        public async Task<ActionResult> ParkVehicle(Vehicle vehicle, Guid spotId)
        {
            var result = await _mainService.ParkVehicle(vehicle, spotId);
            return Ok(result);
        }

        [HttpPost]
        [Route("UnparkVehicle")]
        public async Task<ActionResult> UnParkVehicle(Guid ticketId, PaymentType payTpye)
        {
            var result = await _mainService.UnParkVehicle(ticketId, payTpye);
            return Ok(result);
        }
    }
}
