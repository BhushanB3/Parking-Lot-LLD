using Parking_Spot.Classes.Entities;

namespace Parking_Spot.Classes.Payment
{
    public interface IPayment
    {
        public bool pay(Ticket ticket);
    }
}
