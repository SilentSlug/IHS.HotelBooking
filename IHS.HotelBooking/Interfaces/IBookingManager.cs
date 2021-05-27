using System;

namespace IHS.HotelBooking.Interfaces
{
    public interface IBookingManager
    {
        // Return true if there is no booking for the given room on the date, otherwise false
        bool IsRoomAvailable(int room, DateTime date);

        // Add a booking for the given guest, in the given room, on the given date. If the room is not available, throw a suitable exception.
        void AddBooking(string guest, int room, DateTime date);
    }
}
