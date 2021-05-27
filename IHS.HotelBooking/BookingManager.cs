using IHS.HotelBooking.Interfaces;
using IHS.HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHS.HotelBooking
{
    public class BookingManager : IBookingManager
    {
        private readonly List<Booking> bookings;

        public BookingManager()
        {
            bookings = new List<Booking>();
        }

        public async Task AddBooking(string guest, int room, DateTime date)
        {
            try
            {
                if (await IsRoomAvailable(room, date).ConfigureAwait(false))
                {
                    bookings.Add(new Booking { CustomerSurname = guest, RoomNumber = room, BookingDate = date });
                }
                else
                {
                    throw new BookingException("The room you have selected is unavailable");
                }
            }
            catch
            {
                throw new Exception("An unexpected error occured when adding a booking.");
            }
        }

        public async Task<bool> IsRoomAvailable(int room, DateTime date)
        {
            try
            {
                bool availability = false;

                await Task.Run(() =>
                {
                    availability = !bookings.Any(b => b.RoomNumber == room && b.BookingDate == date);
                }).ConfigureAwait(false);

                return availability;
            }
            catch
            {
                throw new Exception("An unexpected error occured when checking room availability.");
            }
        }
    }
}
