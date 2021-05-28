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
                    //Custom exception felt more suitable
                    throw new BookingException($"Room number {room} is currently unavailable at the selected date: {date}");
                }
            }
            catch(Exception ex)
            {
                //This would normally add to logs (Splunk, local file etc)
                Console.WriteLine(ex.Message);
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
            catch(Exception ex)
            {
                //This would normally add to logs (Splunk, local file etc)
                Console.WriteLine(ex.Message);
                //Suppress sesitive exception message to user by throwing custom exception
                throw new BookingException("An unexpected error occured when checking room availability.");
            }
        }
    }
}
