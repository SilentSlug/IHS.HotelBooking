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

            await Task.Run(() =>
            {
                Console.WriteLine(bookingManager.IsRoomAvailable(101, today).Result);
            }).ConfigureAwait(false);

            await bookingManager.AddBooking("South", 101, today).ConfigureAwait(false);

            await Task.Run(() =>
            {
                Console.WriteLine(bookingManager.IsRoomAvailable(101, today).Result);
            }).ConfigureAwait(false);

            await bookingManager.AddBooking("South", 101, today).ConfigureAwait(false);

            Console.Read();
        }
    }
}
