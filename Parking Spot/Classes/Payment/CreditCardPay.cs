using Parking_Spot.Classes.Entities;
using Parking_Spot.Classes.Payment;

namespace Parking_Spot.Classes
{
    public class CreditCardPay : IPayment
    {
        public bool pay(Ticket ticket)
        {
            ticket.PaymentType = PaymentType.CreditCard;
            return true;
        }
    }
}
