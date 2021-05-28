using IHS.HotelBooking.Interfaces;
using System;
using System.Threading.Tasks;

namespace IHS.HotelBooking
{
    class Program
    {
        //Using async as there was a requirement to efficiently handle threads safely
        static async Task Main()
        {
            IBookingManager bookingManager = new BookingManager();

            var today = DateTime.Today;

            //Get all available rooms
            var availableRooms = await bookingManager.GetAvailableRooms(today).ConfigureAwait(false);

            //Display available rooms to console
            foreach (var room in availableRooms)
            {
                Console.WriteLine(room.ToString());
            }

            //Check if room 101 is available
            await Task.Run(() =>
            {
                Console.WriteLine(bookingManager.IsRoomAvailable(101, today).Result);
            }).ConfigureAwait(false);

            //Add booking for room 101
            await bookingManager.AddBooking("South", 101, today).ConfigureAwait(false);

            //Check if room 101 is available after booking
            await Task.Run(() =>
            {
                Console.WriteLine(bookingManager.IsRoomAvailable(101, today).Result);
            }).ConfigureAwait(false);

            //Check all available rooms after booking
            availableRooms = await bookingManager.GetAvailableRooms(today).ConfigureAwait(false);

            //Display available rooms to console
            foreach (var room in availableRooms)
            {
                Console.WriteLine(room.ToString());
            }

            //Attempt to add booking for an unavailable room
            await bookingManager.AddBooking("South", 101, today).ConfigureAwait(false);

            Console.Read();
        }
    }
}
