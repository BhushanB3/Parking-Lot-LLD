using Parking_Spot.Classes.Entities;
using Parking_Spot.Classes.Payment;

namespace Parking_Spot.Classes
{
    public class DebitCardPay : IPayment
    {
        public bool pay(Ticket ticket)
        {
            ticket.PaymentType = PaymentType.Debitcard;
            return true;
        }
    }
}
