using IHS.HotelBooking.Interfaces;
using System;
using System.Threading.Tasks;

namespace IHS.HotelBooking
{
    public class BookingManager : IBookingManager
    {
        public async Task AddBooking(string guest, int room, DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsRoomAvailable(int room, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
