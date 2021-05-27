using System;

namespace IHS.HotelBooking.Models
{
    public class Booking
    {
        public string CustomerSurname { get; set; }

        public int RoomNumber { get; set; }

        public DateTime BookingDate { get; set; }
    }
}
