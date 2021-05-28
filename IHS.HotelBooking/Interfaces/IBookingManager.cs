using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IHS.HotelBooking.Interfaces
{
    public interface IBookingManager
    {
        // Return true if there is no booking for the given room on the date, otherwise false
        Task<bool> IsRoomAvailable(int room, DateTime date);

        // Add a booking for the given guest, in the given room, on the given date. If the room is not available, throw a suitable exception.
        Task AddBooking(string guest, int room, DateTime date);

        //Return a list of all the available room rumbers for the given date
        Task<IEnumerable<int>> GetAvailableRooms(DateTime date);
    }
}
