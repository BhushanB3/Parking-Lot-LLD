using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Parking_Spot.Classes.Entities
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
        public DateTime EntryTime { get; set; } = DateTime.Now;
        public DateTime? ExitTime { get; set; }
        public double TotalFees { get; set; }
        public bool IsPaid { get; set; } = false;
        public PaymentType? PaymentType { get; set; } = null;
    }
} 
