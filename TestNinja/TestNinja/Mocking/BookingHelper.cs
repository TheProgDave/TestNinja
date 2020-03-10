using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }

    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Status != "Cancelled");
            if (excludedBookingId.HasValue)
                bookings = bookings.Where(b => b.Id != excludedBookingId.Value);
            return bookings;
        }
    }


    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository bookingRepository)
        {
            // If booking is cancelled return immediately
            if (booking.Status == "Cancelled")
                return string.Empty;
            // Get a list of all other active bookings
            var bookings = bookingRepository.GetActiveBookings(booking.Id);
            // Checks if any of the bookings returned overlap with the existing booking
            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate >= b.ArrivalDate
                        && booking.ArrivalDate < b.DepartureDate
                        || booking.DepartureDate > b.ArrivalDate
                        && booking.DepartureDate <= b.DepartureDate);
            // returns empty string if no-overlap returns strung reference to booking if overlapping
            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public class UnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}