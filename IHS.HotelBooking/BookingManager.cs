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
        private readonly List<int> hotelRooms;

        public BookingManager()
        {
            bookings = new List<Booking>();

            //Persistent data storage was not required so this will do
            hotelRooms = new List<int>() { 101, 309, 204, 605 };
        }

        public async Task AddBooking(string guest, int room, DateTime date)
        {
            try
            {
                if (!await IsRoomAvailable(room, date).ConfigureAwait(false))
                {
                    //Custom exception felt more suitable
                    throw new BookingException($"Room number {room} is currently unavailable at the selected date: {date}");
                }

                if (hotelRooms.Contains(room))
                {
                    bookings.Add(new Booking { CustomerSurname = guest, RoomNumber = room, BookingDate = date });
                }
                else
                {
                    throw new BookingException($"Room number {room} does not exist.");
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

        public async Task<IEnumerable<int>> GetAvailableRooms(DateTime date)
        {
            try
            {
                var availableRooms = from hr in hotelRooms
                                     where IsRoomAvailable(hr, date).Result
                                     select hr;

                return await Task.FromResult(availableRooms).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                //This would normally add to logs (Splunk, local file etc)
                Console.WriteLine(ex.Message);
                //Suppress sesitive exception message to user by throwing custom exception
                throw new BookingException("An unexpected error occured when getting available rooms.");
            }
        }
    }
}