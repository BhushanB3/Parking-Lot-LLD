using Parking_Spot.Classes.Entities;
using Parking_Spot.Classes.Payment;

namespace Parking_Spot.Classes
{
    public class TicketService
    {
        public static double BaseRate = 25;
        public static double GetParkingDuration(Ticket ticket)
        {
            var minutes = (DateTime.Now - ticket.EntryTime).TotalMinutes;
            return Math.Round(minutes / 60.0, 2);
        }

        public static double CalculateTotalFees(Ticket ticket)
        {
            var ParkDuration = GetParkingDuration(ticket);
            
            switch(ticket.Vehicle.Type)
            {
                case VehicleType.Car:
                    ticket.TotalFees = BaseRate + Math.Round(ParkDuration * (int)PayRate.Car);
                    return ticket.TotalFees;
                case VehicleType.Bike:
                    ticket.TotalFees = BaseRate + Math.Round(ParkDuration * (int)PayRate.Bike);
                    return ticket.TotalFees;
                case VehicleType.Truck:
                    ticket.TotalFees = BaseRate + Math.Round(ParkDuration * (int)PayRate.Truck);
                    return ticket.TotalFees;
                default:
                    throw new InvalidOperationException("Unknown vehicle type");
            }
        }
    
        public static bool MakePayment(Ticket ticket, PaymentType paymentType, double fees)
        {
            IPayment payobj = PaymentFactory.CreatePayObj(paymentType);
            var paid = payobj.pay(ticket);
            if(paid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public enum PayRate
    {
        Car = 40,
        Bike = 20,
        Truck = 60
    }

    public enum PaymentType
    {
        CreditCard,
        Debitcard,
        UPI
    }
}
