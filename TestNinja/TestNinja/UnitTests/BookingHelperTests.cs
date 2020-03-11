using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class BookingHelperTests
    {
        private Mock<IBookingRepository> _bookingRepository;
        private Booking _existingBooking;


        [SetUp]
        public void SetUp()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _existingBooking = _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 12, 1),
                DepartureDate = DepartOn(2017, 12, 6),
                Reference = "existing_booking_reference"
            };

            _bookingRepository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());
        }
        
        [Test]
        public void OverlappingBookingExists_StartsAndEndsBeforeAnExistingBooking_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, daysBefore: 3),
                DepartureDate = Before(_existingBooking.ArrivalDate, daysBefore: 1),
                Reference = "new_booking_reference",
            }, _bookingRepository.Object);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingExists_StartsAndEndsBeforeExistingBooking_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate, daysAfter: 1),
                DepartureDate = After(_existingBooking.DepartureDate, daysAfter: 3),
                Reference = "new_booking_reference",
            }, _bookingRepository.Object);
            Assert.That(result, Is.Empty);
        }


        [Test]
        public void OverlappingBookingExists_StartsDuringExistingBooking_ReturnsOverlappingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.DepartureDate, daysBefore: 1),
                DepartureDate = After(_existingBooking.DepartureDate, daysAfter:3),
                Reference = "new_booking_reference",
            }, _bookingRepository.Object);
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingExists_EndsDuringExistingBooking_ReturnsOverlappingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, daysBefore: 1),
                DepartureDate = After(_existingBooking.ArrivalDate, daysAfter: 3),
                Reference = "new_booking_reference",
            }, _bookingRepository.Object);
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingExists_StartsBeforeAndEndsAfterExistingBooking_ReturnsOverlappingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, daysBefore: 1),
                DepartureDate = After(_existingBooking.DepartureDate, daysAfter: 3),
                Reference = "new_booking_reference",
            }, _bookingRepository.Object);
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingExists_StartsDuringAndEndsDuringExistingBooking_ReturnsOverlappingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
                Reference = "new_booking_reference",
            }, _bookingRepository.Object);
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        // Helper methods used for reducing clutter in tests
        private DateTime Before(DateTime dateTime, int daysBefore = 1)
        {
            return dateTime.AddDays(-daysBefore);
        }

        private DateTime After(DateTime datetime, int daysAfter = 1)
        {
            return datetime.AddDays(+daysAfter);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year,month,day,14,0,0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}