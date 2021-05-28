using System;

public class BookingException : Exception
{
    public BookingException()
    {
    }

    public BookingException(string message)
        : base(message)
    {
    }
}